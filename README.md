<h1>Comic Shop API</h1>
<h3>Api para uma loja de quadrinhos</h3>

<p>Este é um pequeno projeto que tem como intuito, simular o back-end de uma loja online de ŕevistas em quadrinhos. Para atender aos requisitos da loja, Foi pensando em uma api restful implementada com DotNet 7, Entity Framework e MySQL.</p>

<h4>Como testar a api na minha máquina?</h4>

<ol>
	<li>Clone este repositório;</li>
	<li>No terminal, navegue até o diretório raiz do projeto clonado em sua máquina.</li>
	<li>Crie o arquivo "appsettings.Development.json"</li>
	<li>Adicione as segintes linhas no arquivo, lembrando de substituir as informações genéricas pelas informações reais do seu banco de dados:</li>
		<pre>
		{
  			"ConnectionString": "Host=localhost;Database=comicshop;Username=root;Password="
		}
		</pre>
	<li>Ececute o comando "dotnet restore"</li>
	<li>Ececute o comando "dotnet tool restore"</li>
	<li>Execute o comando "dotnet ef database update"</li>
	<li>execute a aplicação com o comando "dotnet run"</li>
</ol>

<h4>Quais as funcionalidades desta api?</h4>

<ul>
	<li>Cadastro de quadrinhos;</li>
	<li>Leitura de quadrinhos;</li>
	<li>Leitura de um quadrinho específico a partir do Id</li>
	<li>Atualização de um quadrinho</li>
	<li>Remoção de um quadrinho</li>
	<li>Cadastro de usuários</li>
	<li>Login de usuários</li>
	<li>Leitura de usuários</li>
	<li>Leitura de um usuário específico a partir do seu Id</li>
	<li>Compra de quadrinhos</li>
	<li>Leitura da lista de compras de um usuário específico</li>
	<li>Leitura do histórico de vendas de um quadrinho específico</li>
</ul>