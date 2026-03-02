abstract class BaseUser {
    constructor(public id: number, public name: string) {}
  
    abstract getRole(): string;
    abstract getPermissions(): string[];
  }
  
  class Guest extends BaseUser {
    getRole(): string {
      return 'Guest';
    }
  
    getPermissions(): string[] {
      return ['Просмотр контента'];
    }
  }
  
  class User extends BaseUser {
    getRole(): string {
      return 'User';
    }
  
    getPermissions(): string[] {
      return ['Просмотр контента', 'Добавление комментариев'];
    }
  }
  
  class Admin extends BaseUser {
    getRole(): string {
      return 'Admin';
    }
  
    getPermissions(): string[] {
      return [
        'Просмотр контента',
        'Добавление комментариев',
        'Удаление комментариев',
        'Управление пользователями',
      ];
    }
  }
const guest = new Guest(1, "Аноним");
console.log(guest.getPermissions());
console.log(guest.getRole());

const user = new User(2, "Лити");
console.log(user.getPermissions());
console.log(user.getRole());

const admin = new Admin(3, "Admin");
console.log(admin.getPermissions());
console.log(admin.getRole());




interface IReport {
    title: string;
    content: string;
    generate(): string | object;
  }
  
  class HTMLReport implements IReport {
    constructor(public title: string, public content: string) {}
  
    generate(): string {
      return `<h1>${this.title}</h1><p>${this.content}</p>`;
    }
  }
  
  class JSONReport implements IReport {
    constructor(public title: string, public content: string) {}
  
    generate(): object {
      return { title: this.title, content: this.content };
    }
  }

  const report1 = new HTMLReport("Отчет 1", "Содержание отчета");
  console.log(report1.generate()); // <h1>Отчет 1</h1><p>Содержание отчета</p>
  
  const report2 = new JSONReport("Отчет 2", "Содержание отчета");
  console.log(report2.generate()); // { title: 'Отчет 2', content: 'Содержание отчета' }




  class CacheC<T> {
    private storage: {
      [key: string]: { value: T; expire: number };
    } = {};
  
    add(key: string, value: T, ttl: number): void {
      const expire = Date.now() + ttl;
      this.storage[key] = { value, expire };
    }
  
    get(key: string): T | null {
      const item = this.storage[key];
      if (!item) return null;
  
      if (Date.now() > item.expire) {
        delete this.storage[key];
        return null;
      }
  
      return item.value;
    }
  
    clearExpired(): void {
      const now = Date.now();
      Object.keys(this.storage).forEach((key) => {
        if (now > this.storage[key].expire) {
          delete this.storage[key];
        }
      });
    }
  }
  

  const cache = new CacheC<number>();
  cache.add("price", 100, 5000);
  console.log(cache.get("price")); // 100
  
  setTimeout(() => {
    console.log(cache.get("price")); // null (через 6 секунд)
  }, 6000);


  function createInstance<T>(cls: new (...args: any[]) => T, ...args: any[]): T {
    return new cls(...args);
  }
  

  class Product {
    constructor(public name: string, public price: number) {}
  }
  
  const p = createInstance(Product, "Телефон", 50000);
  console.log(p); // Product { name: "Телефон", price: 50000 }




  enum LogLevel {
    INFO = "INFO",
    WARNING = "WARNING",
    ERROR = "ERROR"
  }
  
  type LogEntry = [Date, LogLevel, string];
  
  function logEvent(event: LogEntry): void {
    const [timestamp, level, message] = event;
    console.log(`[${timestamp.toISOString()}] [${level}]: ${message}`);
  }
  
  logEvent([new Date(), LogLevel.INFO, "Система запущена"]);




  enum HttpStatus {
    OK = 200,
    BAD_REQUEST = 400,
    UNAUTHORIZED = 401,
    NOT_FOUND = 404,
    INTERNAL_SERVER_ERROR = 500
  }
  
  type ApiResponse<T> = [status: HttpStatus, data: T | null, error?: string];
  
  function success<T>(data: T): ApiResponse<T> {
    return [HttpStatus.OK, data];
  }
  
  function error(message: string, status: HttpStatus): ApiResponse<null> {
    return [status, null, message];
  }
  

  const res1 = success({ user: "Андрей" });
  console.log(res1); // [200, { user: "Андрей" }]
  
  const res2 = error("Не найдено", HttpStatus.NOT_FOUND);
  console.log(res2); // [404, null, "Не найдено"]

