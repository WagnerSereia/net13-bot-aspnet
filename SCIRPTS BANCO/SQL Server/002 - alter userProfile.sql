/*
   segunda-feira, 10 de setembro de 201820:31:07
   Usuário: sa
   Servidor: .
   Banco de Dados: dbNet13
   Aplicativo: 
*/

/* Para impedir possíveis problemas de perda de dados, analise este script detalhadamente antes de executá-lo fora do contexto do designer de banco de dados.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.userProfile
	DROP CONSTRAINT PK_userProfile
GO
ALTER TABLE dbo.userProfile ADD CONSTRAINT
	PK_userProfile_1 PRIMARY KEY CLUSTERED 
	(
	idUser
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.userProfile
	DROP COLUMN _id
GO
ALTER TABLE dbo.userProfile SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.userProfile', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.userProfile', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.userProfile', 'Object', 'CONTROL') as Contr_Per 