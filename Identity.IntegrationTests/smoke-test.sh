clear
baseUrl="http://localhost:18001"

print() {
  if [[ $5 ]]; then # if %5 ids passed in then use that
    printf "%-100s %-10s %-10s %-10s %-10s\n" $1 $2 $3 $4 $5
  elif [[ $3 -eq $4 ]]; then # if expected and actual match
    printf "%-100s %-10s %-10s %-10s %-10s\n" $1 $2 $3 $4 "true"
  else # expected and actual does NOT match
    printf "%-100s %-10s %-10s %-10s %-10s\n" $1 $2 $3 $4 "false"
  fi
}

# calls get method. $1 = url, $2 = expected
getToken() {
  echo $(curl -s --header 'accept: */*' --header 'content-type:application/json' --request 'POST' $1 --data-raw "$2" | jq -r '.token')
}

head() {
  curl --header "Authorization: Bearer $2" -X 'HEAD' -I $1
}

# calls get method. $1 = url, $2 = authToken, $2 = expected
get() {
  actual=$(curl -s -o /dev/null -w "%{http_code}" --header "Authorization: Bearer $2" --request 'GET' $1)
  print $1 "get" $3 $actual
}

post() {
  actual=$(curl -s -o /dev/null -w "%{http_code}" --header 'accept: */*' --header 'content-type:application/json' --header "Authorization: Bearer $2" --request 'POST' $1 --data-raw "$4")
  print $1 "post" $3 $actual
}

# print header
print "url" "method" "expected" "actual" "result"
print "----------------------------------------------------------------------------------------------------" "----------" "----------" "----------" "----------"

# call before authorization
get "$baseUrl/List/Client" "" 401

# call after authorization
token=$(getToken "$baseUrl/Login" '{ "email": "admin@company.com", "password": "P@ssw0rd", "rememberMe": true }')
get "$baseUrl/List/Exception" $token 200
get "$baseUrl/List/Client" $token 200
get "$baseUrl/user/id/b55ebae9-e380-4d96-97eb-a3d2a3d6fb9a" $token 200
