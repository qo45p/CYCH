-- HR 脈搏(HBFreq); RR 呼吸(BTHFreq)
 --combin three different table 
 -- 321 > 80001
 -- 574 > 80005
 -- 154 > 80006
 -- 178 > 80010

-- 以下是574
select * into vs80005 from (
select COALESCE(twoTable.REC_NO,[dbo].[574SBP].REC_NO) as REC_NO, twoTable.REC_DTM, twoTable.[BTNum] as BT , 
		twoTable.[HBFreq] as HR , twoTable.[BTHFreq] as RR  , [dbo].[574SBP].SBP, [dbo].[574SBP].DBP 
from (
	select COALESCE([dbo].[574HR&RR].REC_NO,[dbo].[574BT].REC_NO) as REC_NO, [dbo].[574BT].[BTNum], 
	[dbo].[574HR&RR].[HBFreq], [dbo].[574HR&RR].[BTHFreq] , [dbo].[574HR&RR].REC_DTM
	From  [dbo].[574BT]
	Full OUTER JOIN [dbo].[574HR&RR] on  [dbo].[574BT].REC_NO = [dbo].[574HR&RR].REC_NO
) AS twoTable
Full OUTER JOIN [dbo].[574SBP] on  [dbo].[574SBP].REC_NO = twoTable.REC_NO
) AS threeTable


---- 以下是321
--select * into vs80001 from (
--select COALESCE(twoTable.REC_NO,[dbo].[321SBP].REC_NO) as REC_NO, twoTable.REC_DTM, twoTable.[BTNum] as BT , 
--		twoTable.[HBFreq] as HR , twoTable.[BTHFreq] as RR  , [dbo].[321SBP].SBP, [dbo].[321SBP].DBP 
--from (
--	select COALESCE([dbo].[321HR&RR].REC_NO,[dbo].[321BT].REC_NO) as REC_NO, [dbo].[321BT].[BTNum], 
--	[dbo].[321HR&RR].[HBFreq], [dbo].[321HR&RR].[BTHFreq] , [dbo].[321HR&RR].REC_DTM
--	From  [dbo].[321BT]
--	Full OUTER JOIN [dbo].[321HR&RR] on  [dbo].[321BT].REC_NO = [dbo].[321HR&RR].REC_NO
--) AS twoTable
--Full OUTER JOIN [dbo].[321SBP] on  [dbo].[321SBP].REC_NO = twoTable.REC_NO
--) AS threeTable


----以下是 154
--select * into vs80006 from (
--select COALESCE(twoTable.REC_NO,[dbo].[154SBP].REC_NO) as REC_NO, twoTable.REC_DTM, twoTable.[BTNum] as BT , 
--		twoTable.[HBFreq] as HR , twoTable.[BTHFreq] as RR  , [dbo].[154SBP].SBP, [dbo].[154SBP].DBP 
--from (
--	select COALESCE([dbo].[154HR&RR].REC_NO,[dbo].[154BT].REC_NO) as REC_NO, [dbo].[154BT].[BTNum], 
--	[dbo].[154HR&RR].[HBFreq], [dbo].[154HR&RR].[BTHFreq] , [dbo].[154HR&RR].REC_DTM
--	From  [dbo].[154BT]
--	Full OUTER JOIN [dbo].[154HR&RR] on  [dbo].[154BT].REC_NO = [dbo].[154HR&RR].REC_NO
--) AS twoTable
--Full OUTER JOIN [dbo].[154SBP] on  [dbo].[154SBP].REC_NO = twoTable.REC_NO
--) AS threeTable

---- 以下是 178
--select * into vs80010 from (
--select COALESCE(twoTable.REC_NO,[dbo].[178SBP].REC_NO) as REC_NO, twoTable.REC_DTM, twoTable.[BTNum] as BT , 
--		twoTable.[HBFreq] as HR , twoTable.[BTHFreq] as RR  , [dbo].[178SBP].SBP, [dbo].[178SBP].DBP 
--from (
--	select COALESCE([dbo].[178HR&RR].REC_NO,[dbo].[178BT].After_REC_NO) as REC_NO, [dbo].[178BT].[BTNum], 
--	[dbo].[178HR&RR].[HBFreq], [dbo].[178HR&RR].[BTHFreq] , [dbo].[178HR&RR].REC_DTM
--	From  [dbo].[178BT]
--	Full OUTER JOIN [dbo].[178HR&RR] on  [dbo].[178BT].After_REC_NO = [dbo].[178HR&RR].REC_NO
--) AS twoTable
--Full OUTER JOIN [dbo].[178SBP] on  [dbo].[178SBP].REC_NO = twoTable.REC_NO
--) AS threeTable