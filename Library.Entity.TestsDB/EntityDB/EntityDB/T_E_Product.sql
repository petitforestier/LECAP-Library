CREATE TABLE [dbo].[T_E_Product]
(
	[ProductId]			BIGINT			NOT NULL IDENTITY (1, 1) PRIMARY KEY,
	[Description]		NVARCHAR(40)	NOT NULL,
	[Ranking]			INT				NOT NULL,
)
