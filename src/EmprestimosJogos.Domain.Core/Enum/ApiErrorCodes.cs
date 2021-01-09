using EmprestimosJogos.Domain.Core.Attributes;
using Microsoft.AspNetCore.Http;

namespace EmprestimosJogos.Domain.Core.Enum
{
    public enum ApiErrorCodes
    {
        #region 400 Status (Bad request)

        /// <summary>
        /// O Id informado é inválido.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status400BadRequest)]
        [Description("O Id informado é inválido.")]
        INVID,

        /// <summary>
        /// Erro no versionamento da Api.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status400BadRequest)]
        [Description("Erro no versionamento da Api.")]
        ERRVERAPI,

        /// <summary>
        /// A ApiKey informada é inválida.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status400BadRequest)]
        [Description("A ApiKey informada é inválida.")]
        INVAPKY,

        /// <summary>
        /// Erros de validação no ModelState.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status400BadRequest)]
        [Description("Erros de validação no ModelState.")]
        MODNOTVALD,

        /// <summary>
        /// Versão da API não suportada.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status400BadRequest)]
        [Description("Versão da API não suportada.")]
        NOTSUPAPIVERS,

        /// <summary>
        /// O campo Email não é um endereço de email válido.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status400BadRequest)]
        [Description("O campo Email não é um endereço de email válido.")]
        INVEMAIL,

        /// <summary>
        /// O login informado é inválido.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status400BadRequest)]
        [Description("O login informado é inválido.")]
        INVLOG,

        /// <summary>
        /// O CPF informado é inválido.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status400BadRequest)]
        [Description("O CPF informado é inválido.")]
        INVCPF,

        /// <summary>
        /// O Perfil informado é inválido.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status400BadRequest)]
        [Description("O Perfil informado é inválido.")]
        INVPERFIL,

        /// <summary>
        /// Número de telefone inválido.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status400BadRequest)]
        [Description("Número de telefone inválido.")]
        INVTEL,

        /// <summary>
        /// Número de telefone celular inválido.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status400BadRequest)]
        [Description("Número de telefone celular inválido.")]
        INVCEL,

        /// <summary>
        /// Nome inválido.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status400BadRequest)]
        [Description("Nome inválido.")]
        INVNOME,

        /// <summary>
        /// Bairro inválido.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status400BadRequest)]
        [Description("Bairro inválido.")]
        INVBAIRRO,

        /// <summary>
        /// Logradouro inválido.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status400BadRequest)]
        [Description("Logradouro inválido.")]
        INVLOGRADOURO,

        /// <summary>
        /// CEP inválido.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status400BadRequest)]
        [Description("CEP inválido.")]
        INVCEP,

        /// <summary>
        /// As senhas precisam ser idênticas.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status400BadRequest)]
        [Description("As senhas precisam ser idênticas.")]
        SENDIV,

        /// <summary>
        /// A senha antiga é inválida
        /// </summary>
        [HttpStatusCode(StatusCodes.Status400BadRequest)]
        [Description("A senha antiga é inválida.")]
        SENANTINV,

        /// <summary>
        /// No mímino 6 caracteres, com ao menos uma letra e um número.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status400BadRequest)]
        [Description("No mímino 6 caracteres, com ao menos uma letra e um número.")]
        SENINV,

        /// <summary>
        /// Token inválido.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status400BadRequest)]
        [Description("Token inválido.")]
        INVTOK,

        /// <summary>
        /// Usuário inválido.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status400BadRequest)]
        [Description("Usuário inválido.")]
        INVUSU,

        /// <summary>
        /// A data inicial não pode ser maior ou igual que a data final.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status400BadRequest)]
        [Description("A data inicial não pode ser maior ou igual que a data final.")]
        ADINPSMF,

        /// Data inicial é obrigatória.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status400BadRequest)]
        [Description("Data inicial é obrigatória.")]
        DTINIREQ,

        /// <summary>
        /// Esse campo é obrigatório, preencha.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status400BadRequest)]
        [Description("Esse campo é obrigatório, preencha.")]
        CAMPOBRG,

        /// <summary>
        /// A data final deve ser maior que a data de início.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status400BadRequest)]
        [Description("A data final deve ser maior que a data de início.")]
        DATAFINALMAIOR,
        CARGAHORIFNALCA1200,

        /// <summary>
        /// A senha deve conter no mínimo 6 caracteres e no mínimo 1 letra e 1 número.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status400BadRequest)]
        [Description("A senha deve conter no mínimo 6 caracteres e no mínimo 1 letra e 1 número.")]
        INVPASS,

        /// <summary>
        /// Digite um endereço de e-mail válido
        /// </summary>
        [HttpStatusCode(StatusCodes.Status400BadRequest)]
        [Description("Digite um endereço de e-mail válido")]
        INVENDEMAIL,

        /// <summary>
        /// A data não pode ser anterior a hoje.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status400BadRequest)]
        [Description("A data não pode ser anterior a hoje.")]
        INVDATA,

        /// <summary>
        /// O jogo informado é inválido.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status400BadRequest)]
        [Description("O jogo informado é inválido.")]
        INVJOGO,

        /// <summary>
        /// O amigo informado é inválido.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status400BadRequest)]
        [Description("O amigo informado é inválido.")]
        INVAMIGO,

        #endregion

        #region 401 Status (Unauthorized)

        /// <summary>
        /// Usuário ou senha inválidos.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status401Unauthorized)]
        [Description("Usuário ou senha inválidos.")]
        INVUSPASS,

        /// <summary>
        /// Senha expirada.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status401Unauthorized)]
        [Description("Senha expirada.")]
        EXPPASS,

        /// <summary>
        /// Tentativas excedidas de autenticação, aguarde e tente novamente mais tarde.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status401Unauthorized)]
        [Description("Tentativas excedidas de autenticação, aguarde e tente novamente mais tarde.")]
        LCKLOG,

        #endregion

        #region 403 Status (Forbidden)

        /// <summary>
        /// Sem permissão para acessar o recurso.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status403Forbidden)]
        [Description("Sem permissão para acessar o recurso.")]
        NOTALLW,

        #endregion

        #region 404 Status (Not found)

        /// <summary>
        /// Recurso não encontrado.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status404NotFound)]
        [Description("Recurso não encontrado.")]
        NOTFND,

        /// <summary>
        /// Usuário não encontrado para o login informado.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status404NotFound)]
        [Description("Usuário não encontrado para o login informado.")]
        USNOTFNDBYLOG,

        /// <summary>
        /// Usuário não encontrado para o e-mail informado.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status404NotFound)]
        [Description("Usuário não encontrado para o e-mail informado.")]
        USNOTFNDBYMAIL,

        /// <summary>
        /// E-mail não confirmado.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status404NotFound)]
        [Description("E-mail não confirmado.")]
        EMLNAOCNFRM,

        /// <summary>
        /// Usuário não encontrado para o CPF informado.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status404NotFound)]
        [Description("Usuário não encontrado para o CPF informado.")]
        USNOTFNDBYCPF,

        /// <summary>
        /// Nenhum usuário encontrado com o id informado.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status404NotFound)]
        [Description("Nenhum usuário encontrado com o id informado.")]
        USNOTFNDBYID,


        /// <summary>
        /// Não foi possível encontrar as configurações do Token JWT.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status404NotFound)]
        [Description("Não foi possível encontrar as configurações do Token JWT.")]
        APKYCONFGREQ,

        /// <summary>
        /// CEP não Encontrado.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status404NotFound)]
        [Description("CEP não encontrado")]
        CEPNOTF,

        #endregion

        #region 409 Status (Conflict) 

        [HttpStatusCode(StatusCodes.Status409Conflict)]
        [Description("Login já cadastrado.")]
        ALULOG,

        /// <summary>
        /// Usuário ja cadastrado, verifique o perfil completo na lista de usuários
        /// </summary>
        [HttpStatusCode(StatusCodes.Status409Conflict)]
        [Description("Usuário ja cadastrado, verifique o perfil completo na lista de usuários.", "Usuário já existe!")]
        USUEXIST,

        /// <summary>
        /// CPF já cadastrado, verifique o perfil completo na lista de usuários
        /// </summary>
        [HttpStatusCode(StatusCodes.Status409Conflict)]
        [Description("CPF já cadastrado, verifique o perfil completo na lista de usuários", "Usuário já existe!")]
        CPFEXIST,

        /// <summary>
        /// Usuário existente.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status409Conflict)]
        [Description("Usuário existente.")]
        EXUSU,

        #endregion

        #region 500 Status (Internal Server Error)

        /// <summary>
        /// Erro ao criar usuário pelo Identity.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status500InternalServerError)]
        [Description("Erro ao criar usuário pelo Identity.")]
        CRUSIDNT,

        /// <summary>
        /// Erro ao alterar senha pelo Identity.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status500InternalServerError)]
        [Description("Erro ao alterar senha pelo Identity.")]
        ERALTSEIDNT,

        /// <summary>
        /// Erro inesperado.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status500InternalServerError)]
        [Description("Erro inesperado.", "Contate o administrador do sistema.")]
        UNEXPC,

        /// <summary>
        /// Senha entre uma das 5 últimas utilizadas anteriormente, escolha outra senha.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status500InternalServerError)]
        [Description("Erro ao alterar senha.")]
        CHGPASS,

        /// <summary>
        /// Erro ao fazer login.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status500InternalServerError)]
        [Description("Erro ao fazer login.")]
        ERRLOG,

        /// <summary>
        /// Erro ao alterar senha pelo Identity na criação do usuário.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status500InternalServerError)]
        [Description("Erro ao alterar senha pelo Identity na criação do usuário.")]
        CHGPASCRIDNT,

        /// <summary>
        /// Erros de validação no ModelState.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status500InternalServerError)]
        [Description("Erro no reset de senha pelo Identity.")]
        RESTPASS,

        /// <summary>
        /// Erro ao executar operação no banco de dados.
        /// </summary>
        [HttpStatusCode(StatusCodes.Status500InternalServerError)]
        [Description("Erro ao executar operação no banco de dados.")]
        ERROPBD,

        /// <summary>
        /// Problema na leitura do token
        /// </summary>
        [HttpStatusCode(StatusCodes.Status500InternalServerError)]
        [Description("Problema na leitura do token.")]
        DECTOK,

        /// <summary>
        /// Erro na geração do token
        /// </summary>
        [HttpStatusCode(StatusCodes.Status500InternalServerError)]
        [Description("Erro na geração do token.")]
        ERRGERTOK,

        /// <summary>
        /// Erro ao converter Guid
        /// </summary>
        [HttpStatusCode(StatusCodes.Status500InternalServerError)]
        [Description("Erro ao converter Guid")]
        GUIDPARSEFAIL,

        /// <summary>
        /// Erro ao converter int
        /// </summary>
        [HttpStatusCode(StatusCodes.Status500InternalServerError)]
        [Description("Erro ao converter int")]
        INTPARSEFAIL,

        /// <summary>
        /// Erro ao atualizar o status do comentario
        /// </summary>
        [HttpStatusCode(StatusCodes.Status500InternalServerError)]
        [Description("Erro ao atualizar o status do comentário.")]
        ERRATUSTSCOM,

        #endregion
    }
}
