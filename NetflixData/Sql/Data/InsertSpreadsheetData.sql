USE DB560;

SELECT $(FilePath) + '\Data';

BULK INSERT Flix.Netflix   
   FROM $(FilePath)
   WITH (FORMATFILE = $(FormatPath));