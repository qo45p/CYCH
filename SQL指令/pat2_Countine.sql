



/*��BT_Max�ʭ� �ϥΤ����kFunction */ 
IF OBJECT_ID('FUN_SO') IS NOT NULL DROP FUNCTION FUN_SO
IF OBJECT_ID('FUN_CO1') IS NOT NULL DROP FUNCTION FUN_CO1
GO
CREATE FUNCTION FUN_BT3(@ID numeric(38, 0))
RETURNS NUMERIC(19,2)
AS
BEGIN


DECLARE @NUM1 NUMERIC(19,2),@ID1 numeric(38, 0),@NUM2 NUMERIC(19,2),@ID2 numeric(38, 0)
SELECT TOP 1 @ID1=ID , @NUM1=BT  FROM pat2_export_dataframe WHERE ID<=@ID AND BT<>0 ORDER BY ID DESC


SELECT TOP 1 @ID2=ID , @NUM2=BT  FROM pat2_export_dataframe WHERE ID>=@ID AND BT<>0 ORDER BY ID ASC
IF @ID2<>@ID1
RETURN @NUM1+(((@NUM2-@NUM1)/(@ID2-@ID1))*(@ID-@ID1))

RETURN @NUM1
END
GO


/*�t�s�@��pat2_Countine��ƪ�*/
SELECT *,DBO.FUN_BT3(ID) as BT_Countine,(CASE  WHEN BT is null  then 0 else 1 END) as BT_Null 
into pat2_Countine FROM pat2_export_dataframe


/*�P�_BT_Max�O�_��0�A�åHBT_Null�ӰO��*/
select (CASE BT_Max  
WHEN 0 then 0
else 1
END) as BT_Null
from pat2_BT 