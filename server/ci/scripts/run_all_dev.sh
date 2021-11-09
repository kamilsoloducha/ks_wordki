# run all sevices in containers
docker-compose -f ../docker/docker-compose.Development.yml up -d --build

#wait until mysql is ready
sleep 20

#read build sql script from file
value=$(<../sqls/1.sql)
echo "$value"

#execute script on mysql container
docker exec -it mysql_dev mysql -u root --password=pass1 -e "$value"