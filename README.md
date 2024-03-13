# Aluga.ai-website
# http://localhost:8002/swagger/index.html

=========================================================================
a pessoa vai mandar a solicitação de conexão, que basicamente a outra pessoa vai poder aceitar ou nao

como vai funcionar? no serviço

VOU TER QUE CRIAR UMA OUTRA CLASSE QUE SERÁ DE CONEXÕES PENDENTES, QUE IRÁ CONTER TODAS AS USERCONNECTIONS DAQUELE USUÁRIO, exemplo:
3 pessoas podem querer se conectar comigo, então todas esses pedidos de conexão vao para a minha lista de conexões pendentes, que basicamente vai conter apenas uma lista dos ids das pessoas


de acordo com o botão que a pessoa que recebeu o pedido clicar, VAI VIR PARA A API UM TRUE OU FALSE JUNTO COM O ID DA PESSOA QUE O USUÁRIO DESEJA ACEITAR/RECUSAR(VOU PEGAR A LISTA E PROCURAR NELA O ID QUE FOI PASSADO NOS PARAMETROS E TRATAR APENAS ELE) que vai ser atribuido para a tabela USERCONNECTION, se ela vier falsa não realiza ela, se tiver true, adiciona o id da pessoa que veio na sua USERCONNECTION


