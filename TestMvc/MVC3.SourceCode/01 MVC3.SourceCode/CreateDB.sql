
/****** Object:  Table [dbo].[User]    Script Date: 03/25/2013 23:15:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](40) NOT NULL,
	[Password] [nvarchar](40) NOT NULL,
	[Phone] [nvarchar](40) NOT NULL,
	[Residential] [int] NOT NULL,
	[FloorNo] [int] NOT NULL,
	[UnitNo] [int] NOT NULL,
	[DoorplateNo] [int] NOT NULL,
	[SubmitTime] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[User] ADD  DEFAULT (getdate()) FOR [SubmitTime]
GO





/****** Object:  Table [dbo].[Address]    Script Date: 03/25/2013 23:16:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Address](
	[AddressID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](40) NOT NULL,
	[Type] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AddressID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


CREATE PROCEDURE [dbo].[prPager]
	@varIdentityName NVARCHAR(200), --主键名称
	@intPageSize INT, ----每页记录数
	@intCurrentCount INT, ----当前记录数量（页码*每页记录数）
	@varTableName NVARCHAR(200), ----表名称
	@varWhere NVARCHAR(800), ----查询条件
	@intTotalCount INT OUTPUT ----记录总数
AS
	DECLARE @sqlSelect    NVARCHAR(2000) ----局部变量（sql语句），查询记录集
	DECLARE @sqlGetCount  NVARCHAR(2000) ----局部变量（sql语句），取出记录集总数
	
	
	SET @sqlSelect = 'SELECT * FROM  ' 
	    + '    (SELECT ROW_NUMBER()  OVER( ORDER BY ' + @varIdentityName +
	    ' DESC) AS RowNumber,* '
	    + '        FROM ' + @varTableName 
	    + '        WHERE ' + @varWhere 
	    + '     ) as  T2 ' 
	    + ' WHERE T2.RowNumber<= ' + STR(@intCurrentCount + @intPageSize) +
	    ' AND T2.RowNumber>' + STR(@intCurrentCount) 
	
	SET @sqlGetCount = 'SELECT @intTotalCount = COUNT(*) FROM ' + @varTableName 
	    + ' WHERE ' + @varWhere
	
	
	EXEC (@sqlSelect) 
	EXEC SP_EXECUTESQL @sqlGetCount,
	     N'@intTotalCount INT OUTPUT',
	     @intTotalCount OUTPUT


GO














