import axios from 'axios';

/**
 * Rota para o recurso Evento
 */
export const eventsResource = '/Evento';
/**
 * Rota para o recurso Listar Minhas Presenças
 */
export const myEventsResource = '/PresencasEvento/ListarMinhas';
/**
 * Rota para o recurso Presenças Evento
 */
export const presencesEventResource = '/PresencasEvento';
/**
 * Rota para o recurso Presenças Evento
 */
export const commentaryEventResource = '/ComentariosEvento';

/**
 * Rota para o recurso Próximos Eventos
 */
export const nextEventResource = '/Evento/ListarProximos';
/**
 * Rota para o recurso Eventos Anteriores
 */
export const previousEventResource = '/Evento/ListarAnteriores';
/**
 * Rota para o recurso Eventos Anteriores
 */
export const comentaryEventResource = '/ComentariosEvento/Evento/';
/**
 * Rota para o recurso Tipos de Eventos
 */
export const eventsTypeResource = '/TiposEvento';
/**
 * Rota para o recurso Instituição
 */
export const institutionResource = '/Instituicao';
/**
 * Rota para o recurso Login
 */
export const loginResource = '/Login';

const localApiUrl = `http://localhost:5000/api`;
// const externallApiUri = ``;
// const externalApiUri = null;

const api = axios.create({
    baseURL: localApiUrl
});



export default api;