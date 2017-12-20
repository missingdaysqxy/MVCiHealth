-- --------------------------------------------------
-- 预约页面使用此视图查询数据
-- --------------------------------------------------
CREATE OR ALTER VIEW [dbo].[V_RESERVATION] AS
	SELECT 
		[R].*,
		[D].[DOCTOR_NM],
		[D].[GENDER],
		[D].[AGE],
		[D].[TEL] ,
		[D].[PHOTO_URL],
		[D].[DISEASE_ID],
		[D].[INTRODUCTION],
		[S].[SECTION_ID],
		[S].[SECTION_NM],
		[P].[PATIENT_NM],
		[E].[EVALUATION_ID]
	FROM [RESERVATION] AS [R]
	INNER JOIN [DOCTOR] AS [D] ON [D].[DOCTOR_ID]=[R].[DOCTOR_ID]
	INNER JOIN [SECTION_TYPE] AS [S] ON [D].[SECTION_ID]=[S].[SECTION_ID]
	INNER JOIN [PATIENT] AS [P] ON [P].[PATIENT_ID]=[R].[PATIENT_ID]
	LEFT JOIN [DOCTOR_EVALUATION] AS [E] ON [E].[RESERVATION_ID]=[R].[RESERVATION_ID];
GO

-- --------------------------------------------------
-- 医生信息页面使用此视图查询数据
-- --------------------------------------------------
CREATE OR ALTER VIEW [dbo].[V_DOCTORINFO] AS
	SELECT 
		[D].*,
		[P].[PATIENT_ID],
		[P].[PATIENT_NM]
	FROM [DOCTOR] AS [D]
	INNER JOIN [RESERVATION] AS [R] ON [R].[DOCTOR_ID]=[D].[DOCTOR_ID]
	INNER JOIN [PATIENT] AS [P] ON [P].[PATIENT_ID]=[R].[PATIENT_ID];
GO

-- --------------------------------------------------
-- 医生评价页面使用此视图查询数据
-- --------------------------------------------------
CREATE OR ALTER VIEW [dbo].[V_EVALUATION] AS
		SELECT 
		[E].*,
		[D].[DOCTOR_NM],
		[D].[LEVEL],
		[P].[PATIENT_NM]
	FROM [DOCTOR_EVALUATION] AS [E]
	INNER JOIN [DOCTOR] AS [D] ON [D].[DOCTOR_ID]=[E].[DOCTOR_ID]
	INNER JOIN [PATIENT] AS [P] ON [P].[PATIENT_ID]=[E].[PATIENT_ID];
GO

-- --------------------------------------------------
-- 删除重名存储过程
-- --------------------------------------------------
IF EXISTS (SELECT name  
   FROM   sysobjects  
   WHERE  name = 'VeryfyPassword'  
   AND type = 'P') 
DROP PROCEDURE [VeryfyPassword] 
GO 

-- --------------------------------------------------
-- 创建存储过程
-- --------------------------------------------------
CREATE PROCEDURE [dbo].[VeryfyPassword]
	@login_nm nvarchar(20),
	@password varchar(20),
	@iscorrect char(1) output,
	@user_id int output
AS
	DECLARE @count int,
			@t_pwd varchar(20),
			@t_vld char(1);
	SET @user_id=-1;
	SELECT @count = COUNT(*) FROM USERINFO WHERE LOGIN_NM = @login_nm;
	IF (@count > 0) BEGIN
		SELECT @user_id=USER_ID, @t_pwd = PASSWORD, @t_vld = VALID FROM USERINFO WHERE LOGIN_NM = @login_nm;
		IF (CAST(@t_pwd AS varbinary) = CAST(@password AS varbinary) AND (@t_vld = 'T' OR @t_vld IS NULL)) BEGIN
			SET @iscorrect = 'T';
		END
		ELSE BEGIN
			SET @iscorrect = 'F'
		END
	END
	ELSE BEGIN
		SET @iscorrect = 'F'
	END
GO

-- --------------------------------------------------
-- 测试存储过程
-- --------------------------------------------------
DECLARE	@user_id Int,
		@iscorrect char(1)
EXEC	[dbo].[VeryfyPassword]
		@login_nm = 'test1',
		@password = 'a12345',
		@iscorrect = @iscorrect OUTPUT,
		@user_id = @user_id OUTPUT
SELECT	@iscorrect as N'@iscorrect',
		@user_id as 'user_id'
GO
