// ==========================
// CONTROLE DE INICIALIZAÇÃO (ANTI DUPLICAÇÃO)
// ==========================
let iniciado = false;

// ==========================
// RECLAMANTES / RECLAMADAS
// ==========================
function adicionarReclamante(idContainer) {
  criarCampoSimples(idContainer, "Reclamante", "adicionarReclamante");
}

function adicionarCampo(idContainer) {
  criarCampoSimples(idContainer, "Reclamada", "adicionarCampo");
}

function criarCampoSimples(idContainer, placeholder, funcaoAdd) {
  const container = document.getElementById(idContainer);

  const div = document.createElement('div');
  div.classList.add('campo');

  div.innerHTML = `
    <input type="text" placeholder="${placeholder}">
    <button type="button" onclick="${funcaoAdd}('${idContainer}')">+</button>
    <button type="button" onclick="removerCampo(this)">-</button>
  `;

  container.appendChild(div);
}

function removerCampo(botao) {
  const container = botao.parentElement.parentElement;
  if (container.children.length > 1) {
    botao.parentElement.remove();
  }
}

// ==========================
// ABAS
// ==========================
function trocarAba(event, aba) {
  document.querySelectorAll('.conteudo').forEach(div => div.style.display = 'none');
  document.getElementById(aba).style.display = 'block';

  document.querySelectorAll('.tab-btn').forEach(btn => btn.classList.remove('active'));
  event.target.classList.add('active');
}

// ==========================
// PRESENTES
// ==========================
function adicionarCampoPresente(idContainer) {
  const container = document.getElementById(idContainer);

  const div = document.createElement('div');
  div.classList.add('campo');

  div.innerHTML = `
    <input type="text" class="nome-presente" placeholder="Nome">
    <input type="text" class="funcao-presente" placeholder="Função">
    <button type="button" onclick="adicionarCampoPresente('${idContainer}')">+</button>
    <button type="button" onclick="removerCampo(this)">-</button>
  `;

  container.appendChild(div);
}

function pegarPresentes(idContainer) {
  return Array.from(document.querySelectorAll(`#${idContainer} .campo`))
    .map(item => {
      const nome = item.querySelector(".nome-presente")?.value?.trim();
      const funcao = item.querySelector(".funcao-presente")?.value?.trim();
      return (nome || funcao) ? `${nome || ""} - ${funcao || ""}` : null;
    })
    .filter(v => v);
}

// ==========================
// ANEXOS
// ==========================
function criarSelect(tipo, placeholder) {
  const select = document.createElement("select");
  select.style.color = "#999";

  const placeholderOption = document.createElement("option");
  placeholderOption.value = "";
  placeholderOption.textContent = placeholder;
  placeholderOption.disabled = true;
  placeholderOption.selected = true;

  select.appendChild(placeholderOption);

  const limite = (tipo === 'peric') ? 5 : 14;

  for (let i = 1; i <= limite; i++) {
    if (tipo === 'insalub' && i === 4) continue;

    const opt = document.createElement("option");
    opt.value = i;
    opt.textContent = `Anexo ${i}`;
    select.appendChild(opt);
  }

  if (tipo === 'peric') {
    const extra = document.createElement("option");
    extra.value = "*";
    extra.textContent = "Anexo *";
    select.appendChild(extra);
  }

  select.addEventListener("change", () => select.style.color = "#333");

  return select;
}

function adicionarAnexo(idContainer, tipo, placeholder) {
  const container = document.getElementById(idContainer);

  const div = document.createElement("div");
  div.classList.add("campo");

  const select = criarSelect(tipo, placeholder);

  const btnAdd = criarBotao("+", () => adicionarAnexo(idContainer, tipo, placeholder));
  const btnRem = criarBotao("-", () => {
    if (container.children.length > 1) div.remove();
  });

  div.append(select, btnAdd, btnRem);
  container.appendChild(div);
}

// ==========================
// DOCUMENTOS
// ==========================
function criarSelectDocumento() {
  const select = document.createElement("select");
  select.style.color = "#999";

  const placeholder = document.createElement("option");
  placeholder.value = "";
  placeholder.textContent = "Selecione os Documentos";
  placeholder.disabled = true;
  placeholder.selected = true;

  select.appendChild(placeholder);

  ["PGR", "EPI", "FDS", "LTCAT", "PCMSO", "ASO"].forEach(op => {
    const opt = document.createElement("option");
    opt.value = op;
    opt.textContent = op;
    select.appendChild(opt);
  });

  select.addEventListener("change", () => select.style.color = "#333");

  return select;
}

function adicionarDocumento(idContainer) {
  const container = document.getElementById(idContainer);

  const div = document.createElement("div");
  div.classList.add("campo");

  const select = criarSelectDocumento();

  const btnAdd = criarBotao("+", () => adicionarDocumento(idContainer));
  const btnRem = criarBotao("-", () => {
    if (container.children.length > 1) div.remove();
  });

  div.append(select, btnAdd, btnRem);
  container.appendChild(div);
}

// ==========================
// UTIL
// ==========================
function criarBotao(texto, acao) {
  const btn = document.createElement("button");
  btn.type = "button";
  btn.textContent = texto;
  btn.onclick = acao;
  return btn;
}

function pegarLista(idContainer) {
  return Array.from(document.querySelectorAll(`#${idContainer} input`))
    .map(i => i.value.trim())
    .filter(v => v);
}

function pegarListaAnexos(idContainer) {
  return Array.from(document.querySelectorAll(`#${idContainer} select`))
    .map(s => s.value)
    .filter(v => v);
}

function pegarListaDocumentos(idContainer) {
  return pegarListaAnexos(idContainer);
}

function formatarData(idCampo) {
  const valor = document.getElementById(idCampo).value;
  if (!valor) return "";

  const [ano, mes, dia] = valor.split("-");
  return `${dia}/${mes}/${ano}`;
}

// ==========================
// TIPO
// ==========================
let tipoSelecionado = "Periculosidade";

function pegarTipo(botao) {
  tipoSelecionado = botao.dataset.tipo;

  document.querySelectorAll(".tab-btn").forEach(b => b.classList.remove("active"));
  botao.classList.add("active");
}

// ==========================
// ENVIO
// ==========================
async function enviarPericulosidade() {
  const dados = {
    tipo: tipoSelecionado,
    numeroPericia: document.getElementById("numero-processo-peric").value,

    reclamantes: pegarLista("lista-reclamantes-peric"),
    reclamadas: pegarLista("lista-reclamadas-peric"),
    anexos: pegarListaAnexos("container-peric"),
    documentos: pegarListaDocumentos("container-documentos-peric"),
    presentesFuncao: pegarPresentes("lista-presentes-peric"),

    localTrabalho: {
      local: document.getElementById("local-peric").value,
      paredes: document.getElementById("paredes-peric").value,
      pisos: document.getElementById("pisos-peric").value,
      iluminacao: document.getElementById("iluminacao-peric").value,
      ventilacao: document.getElementById("ventilacao-peric").value
    },

    endereco: {
      rua: document.getElementById("rua-peric").value,
      numero: document.getElementById("numero-peric").value,
      bairro: document.getElementById("bairro-peric").value,
      cidade: document.getElementById("cidade-peric").value
    },

    dataLaudo: formatarData("data-laudo-peric"),
    dataPericia: formatarData("data-pericia-peric")
  };

  await fetch("/api/laudo-pericu", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(dados)
  });

  alert("Salvo com sucesso!");
}

// ==========================
// INICIALIZAÇÃO
// ==========================
window.addEventListener('DOMContentLoaded', () => {

  if (iniciado) return;
  iniciado = true;

  // PERICULOSIDADE
  adicionarAnexo('container-peric', 'peric', 'Selecione Anexo Periculosidade');
  adicionarDocumento('container-documentos-peric');

  // INSALUBRIDADE
  adicionarAnexo('container-insalub', 'insalub', 'Selecione Anexo Insalubridade');
  adicionarDocumento('container-documentos-insalub');

  // AMBOS
  adicionarAnexo('container-ambos-peric', 'peric', 'Selecione Anexo Periculosidade');
  adicionarAnexo('container-ambos-insalub', 'insalub', 'Selecione Anexo Insalubridade');
  adicionarDocumento('container-documentos-ambos');

});