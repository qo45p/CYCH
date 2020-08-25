
/*float直接轉換成 varchar會出現科學記號 e ，所以必須經由兩次轉換*/
ALTER TABLE [pat2_Countine] ALTER COLUMN Time13 decimal(25, 0);
ALTER TABLE [pat2_Countine] ALTER COLUMN Time13 varchar(25);
