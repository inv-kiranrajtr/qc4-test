/*
[df:title]
アイテム名を全角・半角、大文字・小文字を同値として検索するSQL

[df:description]
同一QCWEBIDかつ全角・半角、大文字・小文字を同値としてアイテム名を検索する。
一致するもののカウント値を返す

以下の条件値は必須です
・QCWebID
・ItemName

$Id: TItemInfoBhv_SelectItemNameCaseNonsensitive.sqll 5719 2012-04-19 yzhengy@JOPS.LOCAL $
*/
-- #TItemInfoCaseNonsensitiveInfo#
-- !TItemInfoNamePmb!
-- !!Decimal QCWebID!!
-- !!String  ItemName!!
-- !!Decimal? ScenarioId!!

SELECT ITEM_INFO_ID, ITEM_NAME
FROM T_ITEM_INFO
WHERE QCWEBID = /*pmb.QCWebID*/8
AND NLSSORT(ITEM_NAME, 'NLS_SORT=JAPANESE_M_CI') = NLSSORT(/*pmb.ItemName*/'NQ1', 'NLS_SORT=JAPANESE_M_CI')
/*IF pmb.ScenarioId != null*/AND (Category_Edit_ID = /*pmb.ScenarioId*/1 OR Category_Edit_ID IS NULL)/*END*/
;
