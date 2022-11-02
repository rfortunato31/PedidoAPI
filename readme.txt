Aplicação de Pedidos desenvolvida en RestApi C#

Onde existem métodos:
GetPedidos - busca todos os pedido e seus itens
GetPedido - busca um pedido através de um id e recupera seus itens também
Id do Pedido
InsertPedido - insere pedido e seus itens
Json de entrada
{
  "id": 0,
  "nomeCliente": "string",
  "emailCliente": "string",
  "pago": true,
  "dataCriacao": "2022-11-02T10:25:06.517Z",
  "itensAdd": [
    {
      "id": 0,
      "idProduto": 0,
      "quantidade": 0
    }
  ]
}
UpdatePedido - atualiza pedido e seus itens
Json de entrada
{
  "id": 0,
  "nomeCliente": "string",
  "emailCliente": "string",
  "pago": true,
  "dataCriacao": "2022-11-02T10:25:06.517Z",
  "itensAdd": [
    {
      "id": 0,
      "idProduto": 0,
      "quantidade": 0
    }
  ]
}
DeletePedido - exclui pedido e sus itens
Id do Pedido

GetProdutos - busca todos os produtos

para testar existe uma pasta do site publicado que pode ser incluido no iis e testado via postman

fiz os endpoints de produto pois meu front-end não estava 100% preferi entregar apenas o que estava 100%