# GetData
Aplicação web que pega informações do client e salva em um banco de dados MariaDB.

# First step #

# Gerar um servidor para rodar o AspNet (para rodar utilizando docker basta rodar o comando abaixo dentro da pasta raiz do projeto onde está o Dockerfile) 
docker build -t getdata/webui .

# Second step #
# Rodar um banco em MariaDB (Abaixo está o comando para rodar utilizando docker, necessário também criar um volume para persistência com o nome MariaDB_DataInfo) #

docker run -d --name MariaDB_DataInfo -e MARIA_DB_USER=adm -e MARIADB_PASSWORD=Secret -e MARIADB_ROOT_PASSWORD=RootSecret --mount type=volume,src=MariaDB_DataInfo,dst=/var/lib/mysql mariadb

# Obs: Necessário gerar o migrations para criar a tabela do banco. #

# Third step
# Com o container aspnet gerado e banco de dados up, basta rodar o container com o seguinte comando. #

docker run -it --rm -p 8000:80 --name AspNet_DataInfo getdata/webui

# Após isso, basta acessar localhost:8000/Home e aplicação estará sendo servida. #
