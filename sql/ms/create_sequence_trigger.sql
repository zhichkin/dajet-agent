
USE [cerberus];
GO

IF NOT EXISTS(SELECT 1 FROM sys.sequences WHERE name = N'_InfoRg9206_SEQ')
BEGIN
	CREATE SEQUENCE _InfoRg9206_SEQ AS numeric(19,0) START WITH 1 INCREMENT BY 1;
END;
GO

IF NOT EXISTS(SELECT 1 FROM sys.triggers WHERE name = N'_InfoRg9206_INSERT')
BEGIN
	EXECUTE(N'CREATE TRIGGER _InfoRg9206_INSERT ON _InfoRg9206
INSTEAD OF INSERT
AS
	IF EXISTS(SELECT 1 FROM inserted WHERE _Fld9207 IS NULL OR _Fld9207 = 0)
	BEGIN
		INSERT _InfoRg9206
			( _Fld9207,   _Fld9214,  _Fld9208,   _Fld9209,   _Fld9210,   _Fld9211,   _Fld9212,   _Fld9213)
		SELECT NEXT VALUE FOR _InfoRg9206_SEQ,
						i._Fld9214, i._Fld9208, i._Fld9209, i._Fld9210, i._Fld9211, i._Fld9212, i._Fld9213
		FROM inserted AS i;
	END
	ELSE
	BEGIN
		INSERT _InfoRg9206
		       ( _Fld9207,   _Fld9214,   _Fld9208,   _Fld9209,   _Fld9210,   _Fld9211,   _Fld9212,   _Fld9213)
		SELECT i._Fld9207, i._Fld9214, i._Fld9208, i._Fld9209, i._Fld9210, i._Fld9211, i._Fld9212, i._Fld9213
		FROM inserted AS i;
	END;');
END;
GO

--BEGIN TRANSACTION;

--SELECT _Fld3155  AS [�������������],
--NEXT VALUE FOR _InfoRg3154_SEQ OVER (ORDER BY _Fld3155 ASC) AS [��������������]
--INTO #_InfoRg3154_Copy
--FROM _InfoRg3154 WITH (TABLOCKX,HOLDLOCK);

----SELECT * FROM #_InfoRg3154_Copy;

--UPDATE T
--SET T._Fld3155 = C.[��������������]
--FROM _InfoRg3154 AS T
--INNER JOIN
--#_InfoRg3154_Copy AS C
--ON T._Fld3155 = C.[�������������];

--COMMIT TRANSACTION;

--DROP TABLE #_InfoRg3154_Copy;