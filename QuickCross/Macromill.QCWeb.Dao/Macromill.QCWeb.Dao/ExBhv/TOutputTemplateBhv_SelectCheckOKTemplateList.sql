-- #TOutputTemplateCheckOKTemplate#
-- !TOutputTemplateCheckOKTemplatePmb!
-- !!Decimal QcWebId!!

SELECT
	T.Output_Template_ID
	,T.QCWebID
	,T.Output_Template_Master_ID
	,T.Upload_Path
	,T.Alias
FROM
	T_Output_Template T
WHERE
		T.QCWebID = /*pmb.QcWebId*/320
	AND T.Delete_Flag = 0
	AND EXISTS(
		SELECT 'X' FROM T_Output_Template_Master M WHERE M.Output_Template_Master_ID = T.Output_Template_Master_ID
	)
;