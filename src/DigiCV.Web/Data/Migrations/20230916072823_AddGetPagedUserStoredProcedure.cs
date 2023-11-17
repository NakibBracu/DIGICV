using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigiCV.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddGetPagedUserStoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
	        var getPagedUserData = @"CREATE OR ALTER PROCEDURE [dbo].[GetPagedUserData]
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
											    
											SET @sql ='SELECT up.Id, u.FullName, up.ImageUrl, uc.ClaimType, up.IsActive
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
									END";

	        migrationBuilder.Sql(getPagedUserData);
	        
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5f4c76d3-79b0-4923-86a7-511ac60c2ab9"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "9d234fdb-80e8-4afe-8022-b1711106924f", new DateTime(2023, 9, 16, 13, 28, 22, 814, DateTimeKind.Local).AddTicks(6073), "AQAAAAIAAYagAAAAEPQzVvp+Zr4K+9Rj4Rh+btGXvfNya2kdx9OwZwuETPY7M0hT0x0xnW0ML6p1WxFkRQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e26967f0-ce4c-4c14-8a0b-45beb8c9eb48"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "e1dab3c8-beae-46aa-84d7-2d5c72655130", new DateTime(2023, 9, 16, 13, 28, 22, 664, DateTimeKind.Local).AddTicks(576), "AQAAAAIAAYagAAAAEPmZ6Z78MlbrnhHlw3nYLSKWVtwMV+ERhKnvYygDRSWQJs/YqfSxfSeaYoY1XUSH9g==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5f4c76d3-79b0-4923-86a7-511ac60c2ab9"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "05085a81-4e0f-49fe-a480-fa6a1407a327", new DateTime(2023, 9, 16, 10, 53, 52, 208, DateTimeKind.Local).AddTicks(1991), "AQAAAAIAAYagAAAAECL7gE7UGtYem1bE7zKupH6lrgC5DYXwcLz2jVCjdNuvBDipfwRjORf0/N4gzeb73g==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e26967f0-ce4c-4c14-8a0b-45beb8c9eb48"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "6f2fe156-dc1b-4ab4-af6a-57c9738a09e7", new DateTime(2023, 9, 16, 10, 53, 52, 85, DateTimeKind.Local).AddTicks(4146), "AQAAAAIAAYagAAAAEG/+j6fyDlNXMz0z1LLwwBpTXCd+4fbfz/RDuHK4WM+dCgn5Qqmjwovs7MiTUBiezQ==" });
        }
    }
}
