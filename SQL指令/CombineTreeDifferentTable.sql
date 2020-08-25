--combin different table
SELECT
    COALESCE(BT.Time13, HB.Time13) AS Time13,
    [BTNum],
    [BTHFreq],
	[HBFreq]
FROM (select [BTNum] ,[���q����ϴ��q�ɶ�],[Time13] from [dbo].[154BT]) as BT
FULL OUTER JOIN (select [BTHFreq],[HBFreq] ,[���q����ϴ��q�ɶ�],[Time13] from [dbo].[154HB&BT]) as HB 
ON BT.Time13 = HB.Time13 



-- HR �߷i RR �I�l
 --combin three different table 
 -- 321 > 80001
 -- 574 > 80005
 -- 154 > 80006
 -- 178 > 80010
 Select * into vs80006 from (
 select  COALESCE(twoTable.Time13, SBP.Time13) AS Time13,
    [BTNum] as BT,
    [BTHFreq] as RR,
	[HBFreq] As HR,
	SBP,
	DBP
 from (
 select COALESCE(BT.Time13, HB.Time13) AS Time13,
    [BTNum],
    [BTHFreq],
	[HBFreq]
FROM (select [BTNum] ,[���q����ϴ��q�ɶ�],[Time13] from [dbo].[154BT]) as BT
FULL OUTER JOIN (select [BTHFreq],[HBFreq] ,[���q����ϴ��q�ɶ�],[Time13] from [dbo].[154HB&BT]) as HB  
ON BT.Time13 = HB.Time13 )  as twoTable
FULL OUTER JOIN (select SBP,DBP ,[���q����ϴ��q�ɶ�],[Time13] from [dbo].[154SBP&DBP]) as SBP
on twoTable.Time13 = SBP.Time13
) as allTable


