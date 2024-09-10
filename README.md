#Desafio Tecnico
<br/>
##como rodar
<br/>
clonar repositorio
<br/>
depois rodar o arquivo Script no banco de dados SQL que se encontra  na pasta Script
<br/><br/>
<img width="959" alt="image" src="https://github.com/user-attachments/assets/9abe555d-7945-453f-a8f5-101994c06054">
<br/><br/>
arquivo responsavel por criar o banco de dados, tabela e usuario que sera utilizado na raspagem
<br/><br/>
Altera a variavel DataSource com o nome do servidor sql
<img width="958" alt="image" src="https://github.com/user-attachments/assets/da59d27a-45cf-4833-8048-23d8b4c81026">
<br/>
<img width="352" alt="image" src="https://github.com/user-attachments/assets/939f30ab-e4e1-477b-ad05-2a47bcf45ea5">
no meu caso  DataSource = "VC0003\\SQLEXPRESS01",

apos rodar o script inicar a automação.<br/>
ao iniciar a automação vai abrir o console e sera necessario digitar qual o curso que sera raspado no site
<br/>
<img width="959" alt="image" src="https://github.com/user-attachments/assets/27e30669-d7c1-4e84-b431-ed170fa3bd6d">
<br/><br/>
depois de digitar a automação vai inicar a navegação, raspagem dos dados e insersação no banco de dados, caso um titulo ja exista no banco a automação nao duplica o dado.
<br/><br/>
no final a automação fecha o navegador e apresenta a mensagem "Automação concluída com sucesso!"

