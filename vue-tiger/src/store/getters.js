const getters = {
  sidebar: state => state.app.sidebar,
  device: state => state.app.device,
  token: state => state.user.token,
  avatar: state => state.user.avatar,
  name: state => state.user.name,
  userName: state => state.user.userName,
  introduction: state => state.user.introduction,
  roles: state => state.user.roles,
  email: state => state.user.email,
  phoneNumber: state => state.user.phoneNumber,
  language: state => state.app.language,
  abpConfig: state => state.app.abpConfig,
  permission_routes: state => state.permission.routes,
}
export default getters
