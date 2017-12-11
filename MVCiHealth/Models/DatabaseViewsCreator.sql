-- --------------------------------------------------
-- 预约页面使用此视图查询数据
-- --------------------------------------------------
CREATE OR ALTER VIEW [dbo].[V_RESERVATION] AS
SELECT 
	[R].*,
	[D].[DOCTOR_NM],
    [GENDER],
    [AGE],
    [TEL] ,
    [PHOTO_URL],
    [DISEASE_ID],
    [INTRODUCTION],
	[S].[SECTION_ID],
	[S].[SECTION_NM]
	FROM [RESERVATION] AS [R]
	INNER JOIN [DOCTOR] AS [D] ON [D].[DOCTOR_ID]=[R].[DOCTOR_ID]
	INNER JOIN [SECTION_TYPE] AS [S] ON [D].[SECTION_ID]=[S].[SECTION_ID];
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