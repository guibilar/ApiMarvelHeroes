
# Marvel Heroes

Este projeto foi desenvolvido como refatoração de um projeto desenvolvido por mim há alguns anos atrás, o [Marvel Heroes MVC](https://github.com/guibilar/Marvel "Marvel Heroes MVC"). Através do mesmo é possível consultar personagens e quadrinhos na [API pública da Marvel](https://developer.marvel.com/ "API pública da Marvel") e salva-los em um banco de dados da aplicação, assim, tornando possível edita-los e deleta-los.

Este repositório em configuração de continuos deploy com um Azure Web App, este já condigurado ao Azure SQL e outros recursos de cloud, dessa maneira é possível sempre conferir a última versão públicada no link [aplicação publicada](https://marvelheroesapi20210411203526.azurewebsites.net/swagger/index.html "Marvel Heroes API").

A intenção da refatoração é abandonar a estrutura monilítica e adotando uma arquitetura de camadas dedicadas a responsabilidades espécificas (acesso a banco de dados, camada de regras de négocio, camada de integração e camada de api). Esse tipo de arquitetura de software facilita não apenas a manutenção de código como trás mais beneficios, cada uma das camadas podem ser exportadas e utilizadas em outros projetos, bem como a atribuição única de responsabilidades facilita o gerenciamento de dependencias.

Além de uma nova abordagem na arquitetura também foram adotadas melhores práticas de desenvolvimento de software em comparação com o antigo projeto, seguindo diretrizes do [Clean Code (Robert C. Martin)](https://en.wikipedia.org/wiki/Robert_C._Martin "Clean Code (Robert C. Martin)"), melhores práticas de segurança na gestão e armazenamento das chaves, utilização de injeção de dependencias, retorno de endpoints com formato únificado, repasse de notificações com mensagens já tratadas e setup completo do swagger com autenticação JTW.

### Melhorias e novas funcionalidades
A intenção é expandir as funcionalidades da aplicação possíbilitando a importação completa do banco de personagens da Marvel com uma única ação. A pretenção é utilizar treads e paralelismo para paginar e consultar e salvar todos os personagens no banco de dados da aplicação.
