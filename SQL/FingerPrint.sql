/****** Object:  Table [dbo].[FingerPrint]    Script Date: 9/23/2017 7:31:54 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[FingerPrint](
	[account_id] [int] NOT NULL,
	[finger_print] [nvarchar](50) NOT NULL,
	[ban] [int] NOT NULL,
	[expiration_date] [datetime] NOT NULL
) ON [PRIMARY]

GO

