/*
[df:title]
利用可能なカラム番号を返す

[df:description]
指定されたテーブルの利用可能カラム番号を返す

以下の条件値は必須です
・QCWebID
・TableNo

$Id: TItemInfoBhv_AvailableNo.sql 5719 2012-04-19 mkomatsu@JOPS.LOCAL $
*/
-- #TAvailableNoEntity#

-- !TAvailableTableInfo!
-- !!Decimal QCWebID!!
-- !!int TableNo!!


SELECT
	MIN( T1.COLUMN_NO  + 1 ) AS AVAILABLE_NO
FROM
	T_ITEM_INFO T1
WHERE
	T1.TABLE_NO = /*$pmb.TableNo*/1
		AND
	T1.QCWEBID = /*$pmb.QCWebID*/1101
		AND
	NOT EXISTS (
		SELECT 
			T2.COLUMN_NO
		FROM
			T_ITEM_INFO T2
		WHERE
			T2.TABLE_NO = /*$pmb.TableNo*/1
			AND
			T2.QCWEBID = /*$pmb.QCWebID*/1101
      AND
      ( T1.COLUMN_NO +1 ) = T2.COLUMN_NO
	);
