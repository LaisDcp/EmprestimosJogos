# Getting Started
Para subir a aplicação através do docker-compose:
- Dentro da pasta do projeto, execute o seguinte comando no prompt de comando: <b>docker-compose build</b>
- Em seguida execute o comando: <b>docker-compose up -d</b>
- Para subir o banco da aplicação, execute o comando: <b>docker exec -it emprestimosJogos-mssqlserver /bin/bash /opt/entrypoint.sh</b>
- Para visualizar o swagger e utilizar os endpoints acesse: <b>http://localhost:5000/documentation/index.html</b> 
