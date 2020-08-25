/*新增欄位*/
ALTER TABLE [dbo].[pat2_Countine] ADD PatientID varchar(10)
update [dbo].[pat2_Countine]  set PatientID = '1' where PatientID IS NULL

/*刪除欄位*/
ALTER TABLE [dbo].[pat2_Countine] DROP COLUMN  PatientID

/*刪除Null值*/
DELETE FROM [dbo].[pat2_Countine]
WHERE [BT_Countine] is null