USE [DigiCV]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetPagedUserData]
@PageIndex INT,
@PageSize INT,
@SearchText NVARCHAR(250) = '%',
@OrderBy NVARCHAR(50),
@Total INT OUTPUT,
@TotalDisplay INT OUTPUT
AS
BEGIN
    Declare @sql nvarchar(2000);
	Declare @countsql nvarchar(2000);
	Declare @paramList nvarchar(MAX); 
	Declare @countparamList nvarchar(MAX);

    SET NOCOUNT ON;

	Select @Total = count(*) from AspNetUsers; 
    SET @countsql = 'SELECT @TotalDisplay = COUNT(*) FROM AspNetUsers AS u
        LEFT JOIN UserProfile AS up ON u.Id = up.Id
        LEFT JOIN AspNetUserClaims AS uc ON u.Id = uc.UserId where 1 = 1 ';
		IF @SearchText IS NOT NULL
		SET @countsql = @countsql + ' AND u.FullName LIKE ''%'' + @xSearchText + ''%'' OR 
		up.ImageUrl LIKE ''%'' + @xSearchText + ''%'' OR
		uc.ClaimType LIKE ''%'' + @xSearchText + ''%'''




		--set @sql ='	SELECT up.Id, u.FullName, up.ImageUrl,  uc.ClaimType, up.IsActive
		--FROM AspNetUsers AS u
		--LEFT JOIN UserProfile AS up ON u.Id = up.Id
		--LEFT JOIN AspNetUserClaims AS uc ON u.Id = uc.UserId 
		--WHERE 1 = 1 ';
		SET @sql ='SELECT u.FullName, up.ImageUrl, uc.ClaimType, up.IsActive
		FROM AspNetUsers AS u
		LEFT JOIN UserProfile AS up ON u.UserProfileId = up.Id
		LEFT JOIN AspNetUserClaims AS uc ON u.Id = uc.UserId WHERE 1 = 1 ';
	IF @SearchText IS NOT NULL
    Set @sql = @sql + ' AND u.FullName LIKE ''%'' + @xSearchText + ''%'' OR 
		up.ImageUrl LIKE ''%'' + @xSearchText + ''%'' OR
		uc.ClaimType LIKE ''%'' + @xSearchText + ''%'''

    SET @sql = @sql + ' Order by '+@OrderBy+' OFFSET @PageSize * (@PageIndex - 1) 
	ROWS FETCH NEXT @PageSize ROWS ONLY';





    SELECT @countparamlist = '@xSearchText NVARCHAR(250), @TotalDisplay int output' ;
	exec sp_executesql @countsql, @countparamlist, 
	@SearchText, 
	@TotalDisplay = @TotalDisplay output;

    SELECT @paramlist = '@xSearchText NVARCHAR(250), 
	    @PageIndex int,
		@PageSize int';

	exec sp_executesql @sql , @paramlist ,
	@SearchText, 
	@PageIndex,
	@PageSize;

	print @countsql;
	print @sql;
END
