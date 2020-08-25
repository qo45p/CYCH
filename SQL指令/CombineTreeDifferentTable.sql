--combin different table
SELECT
    COALESCE(BT.Time13, HB.Time13) AS Time13,
    [BTNum],
    [BTHFreq],
	[HBFreq]
FROM (select [BTNum] ,[代秖ら戳∠代秖丁],[Time13] from [dbo].[154BT]) as BT
FULL OUTER JOIN (select [BTHFreq],[HBFreq] ,[代秖ら戳∠代秖丁],[Time13] from [dbo].[154HB&BT]) as HB 
ON BT.Time13 = HB.Time13 



-- HR 穒 RR ㊣
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
FROM (select [BTNum] ,[代秖ら戳∠代秖丁],[Time13] from [dbo].[154BT]) as BT
FULL OUTER JOIN (select [BTHFreq],[HBFreq] ,[代秖ら戳∠代秖丁],[Time13] from [dbo].[154HB&BT]) as HB  
ON BT.Time13 = HB.Time13 )  as twoTable
FULL OUTER JOIN (select SBP,DBP ,[代秖ら戳∠代秖丁],[Time13] from [dbo].[154SBP&DBP]) as SBP
on twoTable.Time13 = SBP.Time13
) as allTable


