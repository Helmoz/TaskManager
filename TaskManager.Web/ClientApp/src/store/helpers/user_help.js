export default class User {
  constructor(id, email, name, photoUrl) {
    this.id = id
    this.email = email
    ;(this.name = name), (this.photo = photoUrl)
  }
}
