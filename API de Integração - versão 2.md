# API de Integração - versão 2

Esta documentação abrange os endpoints da API de Integração Versão 2, que são fundamentais para a comunicação entre o back-end e o Dashboard Versão 2. Os endpoints são projetados para fornecer uma manipulação avançada de dados, incluindo a recuperação, filtragem e análise, para facilitar uma experiência de usuário aprimorada no dashboard.

obs.: teste de deploy

# 1. Endpoints

Endpoints são pontos de interação ou interfaces de comunicação em sistemas de software, especialmente em arquiteturas de serviços web, APIs (Application Programming Interfaces) ou sistemas de rede. Eles atuam como portas de acesso através das quais os serviços oferecidos por um software podem ser acessados por outros softwares, sistemas ou usuários.

Eles são fundamentais para a construção de aplicações interativas e distribuídas, pois permitem a separação entre a interface (frontend) e a lógica de negócio e dados (backend), facilitando o desenvolvimento, a manutenção e a escalabilidade das aplicações.

Abaixo estão explícitos os endpoints da API até o momento:

### 1.1. Dados do Stiba

- **URI**: **`GET /api/Stiba/stiba`**
- **Método HTTP**: GET
- **Descrição**: Oferece uma coleção de respostas coletadas pelo serviço Stiba, estruturadas para análise e visualização no dashboard.
- **Parâmetros de entrada**: Nenhum.
- **Formato de Resposta**:
    
    ```csharp
    [
      {
        "descricaoUO": "B COO - ... OPERATING OFFICER VWB",
        "elegiveis": 10.923,
        "respondentes": 10.473,
        "percPart": null,
        "notaStiba": "86,6",
        "q01": "81,5",
        "q02": "86,2",
        "q03": "82,7",
        "q04": "83",
        "q05": "84,9",
        "q06": "86,4",
        "q07": "81,6",
        "q08": "91",
        "q09": "86",
        "q10": "86,5",
        "q11": "86,6",
        "q12": "83,9",
        "q13": "85,8",
        "q14": "91,9",
        "q15": "84,8",
        "q16": "84,5",
        "q17": "86,3",
        "q18": "91,1",
        "q19": "91,5",
        "q20": "93,6",
        "q21": "84,1",
        "q22": "92,3",
        "q23": "91,8",
        "q24": "85,7"
      },
    
- **Respostas**:
    - **200 OK**: Dados recuperados com sucesso.
    - **500 Internal Server Error**: Falha devido a um erro interno do servidor, com detalhes fornecidos na mensagem de erro.

### 1.2. Dados do Cid

- **URI**: **`GET /api/Cids/cids`**
- **Método HTTP**: GET
- **Descrição**: Retorna uma lista de dados Cids, que inclui informações detalhadas como unidade, diretoria, dias de atestado, diagnósticos e categorias de CID.
- **Parâmetros de entrada**: Nenhum.
- **Formato de Resposta**:
    
```csharp
[
  {
    "mes": "Out",
    "n_Pessoal": 4020504,
    "atestados": 1,
    "dias": "15",
    "diretoria": "OPERAÇÕES",
    "unidade": "SCA",
    "genero": "Masculino",
    "categoria": "HD",
    "cid": "F430",
    "descricao_Detalhada": "Reação aguda ao \"stress\"",
    "descricao_Resumida": null,
    "diagnostico_Atestado_Inicial": null,
    "causa_Raiz": null,
    "outros": null,
    "jornada": null
  },
 ```   
- **Respostas**:
    - **200 OK**: Sucesso na recuperação dos dados.
    - **500 Internal Server Error**: Erro interno do servidor, geralmente acompanhado de uma mensagem explicativa.

# 2. Exemplos de Uso

### **2.1. Recuperando Dados do Cids**

Para recuperar os dados do Cids, envie uma solicitação GET para o endpoint **`/api/Cids/cids`**. Não são necessários parâmetros. A resposta incluirá um array de objetos **`CidsModel`**, cada um representando um conjunto de dados Cids.

```csharp
GET /api/Cids/cids
```
### **2.2. Recuperando Dados do Stiba**

Para obter as respostas do Stiba, faça uma solicitação GET para **`/api/Stiba/stiba`**. Este endpoint retorna uma lista de objetos **`StibaModel`**, contendo as respostas do Stiba e outras métricas relacionadas.

```csharp
GET /api/Stiba/stiba
```

A API utiliza códigos de status HTTP para indicar o resultado das operações. Em caso de erro, uma mensagem descritiva é retornada para auxiliar no diagnóstico e resolução do problema. Por exemplo, um erro 500 pode ser acompanhado por uma mensagem como "Erro ao obter Cids", indicando um problema no processamento dessa solicitação específica.

# Conclusão

Esta documentação oferece uma visão clara dos endpoints disponíveis na API de Integração Versão 2, incluindo detalhes sobre os métodos HTTP suportados, parâmetros de entrada e formatos de resposta. Com essa API, o Dashboard Versão 2 pode oferecer uma experiência de usuário mais rica e interativa, permitindo uma análise de dados avançada e insights mais profundos.
