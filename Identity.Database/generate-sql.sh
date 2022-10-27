schema="dbo"
table="Role"

columns=""
while IFS=, read -r Column Datatype Required Length FkTable FkColumn; do
  # datatype
  if [ "$Datatype" == "nvarchar" ]; then
    DatatypString="$Datatype($Length)"
  else
    DatatypString="$Datatype"
  fi
  # nullable
  if [ "$Required" == "1" ]; then
    NullableString="NOT NULL"
  fi

  columns+="
    , $Column $DatatypString $NullableString"
done < <(tail -n +2 $schema/$table.csv)

echo "IF NOT EXITS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='$schema' AND TABLE_NAME='$table')
BEGIN
  CREATE TABLE $schema.$table (
    ${columns:7}
  )
END" >dbo/Role.sql
