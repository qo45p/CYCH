/*�s�W���*/
ALTER TABLE [dbo].[pat2_Countine] ADD PatientID varchar(10)
update [dbo].[pat2_Countine]  set PatientID = '1' where PatientID IS NULL

/*�R�����*/
ALTER TABLE [dbo].[pat2_Countine] DROP COLUMN  PatientID

/*�R��Null��*/
DELETE FROM [dbo].[pat2_Countine]
WHERE [BT_Countine] is null