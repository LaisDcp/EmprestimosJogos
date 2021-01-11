# Getting Started
Para subir a aplicação atraves do docker-compose:
- Dentro da pasta do projeto, execute o seguinte comando no prompt de comando: docker-compose build
- Em seguida execute o comando: docker-compose up -d
- Para subir o banco da aplicação, execute o comando: docker exec -it emprestimosJogos-mssqlserver /bin/bash /opt/entrypoint.sh
- Para visualizar o swagger e utilizar os endpoints acesse: http://localhost:5000/documentation/index.html 
