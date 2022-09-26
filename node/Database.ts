var mysql = require("mysql");

export class Database {
  conn: any;

  setConnection(host: string, user: string, password: string) {
    this.conn = mysql.createConnection({
      host: host,
      user: user,
      password: password,
    });
  }

  select() {
    this.conn.query();
  }

  insert() {
    this.conn.query();
  }
}
