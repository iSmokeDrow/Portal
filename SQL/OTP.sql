/****** Object:  Table [dbo].[OTP]    Script Date: 9/23/2017 7:33:01 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OTP](
	[account_id] [int] NOT NULL,
	[otp] [nvarchar](50) NOT NULL,
	[expiration] [datetime] NULL
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[OTP] ADD  CONSTRAINT [DF_OTP_expiration]  DEFAULT ('1999-01-01 12:00:00') FOR [expiration]
GO

