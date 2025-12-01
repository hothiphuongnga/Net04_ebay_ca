├── Api/Presentation/                               → LAYER NHẬN REQUEST – TRẢ RESPONSE
│      ├── Controllers/                         → OrderController (chỉ gọi AppService, KHÔNG chứa nghiệp vụ)
│      ├── DTOs/                                → OrderDto (request/response model, KHÔNG chứa logic)
│      ├── Filters/                             → Validate, Exception, Authorization filter
│      ├── Middleware/                          → Middleware (logging, error handling, cors…)
│      └── Program.cs                           → Config DI, middleware, map controllers, endpoints, swagger…
│
├── Application/                                → USE CASE LAYER (Orchestration – điều phối nghiệp vụ)
│      ├── Interfaces/                          → IOrderAppService (chỉ định nghĩa hành vi use case)
│      ├── Services/                            → OrderAppService (dùng Repo )
│      ├── Commands/                            → (CQRS - ghi) CreateOrderCommand, Handler
│      ├── Queries/                             → (CQRS - đọc) GetOrderQuery, Handler
│      └── Mapping/                             → AutoMapper Profile (map DTO ↔ Entity)
│
├── Domain/                                     → LAYER NGHIỆP VỤ THUẦN (DDD TACTICAL)
│      ├── Entities/                            → Order, OrderItem… (có Id, chứa rule nội tại)
│      ├── ValueObjects/                        → Money, Email… (bất biến, không Id)
│      ├── Aggregates/                          → OrderAggregate (Aggregate Root + consistency rules)
│      ├── DomainEvents/                        → OrderCreatedEvent, StudentEnrolledEvent…
│      ├── Interfaces/                          → Repository interface (IOrderRepository… KHÔNG implement)
│      └── Services/                            → Domain Services (tính lương, tính hoa hồng → pure logic)
│                                               → *KHÔNG phụ thuộc DB, EF, API*
│
├── Infrastructure/                             → LAYER KỸ THUẬT – I/O – DATABASE – EXTERNAL
│      ├── Persistence/                         → DbContext, EntityConfig, Transaction
|      ├── Repositories/                        → Implement Repository (OrderRepository : IOrderRepository)
|      ├── Migrations/                          → EF Core Migrations
|      └── Services/                            → EmailService, FileStorage, CacheService…
├── Shared/                                     → LAYER CHUNG (DÙNG CHUNG CHO CÁC LAYER KHÁC) 
│      ├── Common/                              → Paging, Sorting, Filtering, Result classes…
│      ├── Exceptions/                          → Custom exceptions (DomainException, NotFoundException…)
│      └── Utilities/                           → Helper classes (DateTimeProvider, HashingHelper…)


<details>
<summary>✅ 1. Lưu ý về Application Layer</summary>

**ApplicationService chỉ “điều phối”:**
- Gọi Repository  
- Gọi MailService (qua interface)  
- Gọi PaymentGateway (qua interface)  
- Publish domain events  

**❌ Application KHÔNG được:**
- Gọi DbContext trực tiếp → phải qua Repository  
- Mapping lung tung → dùng AutoMapper  

</details>


<details>
<summary>✅ 2. Lưu ý về Domain Layer</summary>

**Domain = trái tim dự án, không phụ thuộc bất kỳ framework nào**

**Domain chứa:**
- Entities (rule nội tại – invariants)  
- ValueObjects  

**❌ KHÔNG được đưa vào Domain:**
- DTO / ViewModel  
- Connection string  
- HttpClient  
- Logger  
- Attribute EF (Key, Table…)  
- LINQ-to-DB query  

➡ Cấu hình EF phải đặt tại: `Infrastructure/Persistence/EntityConfig`

</details>


<details>
<summary>✅ 3. Lưu ý về Infrastructure Layer</summary>

Tầng này chứa toàn bộ **IO, kết nối, giao tiếp bên ngoài**:

- EF Core DbContext  
- Migrations  
- Repository implement  
- EmailService, FileStorage  
- Redis Cache  
- KeyVault/AWS Secret  
- Logging adapter  
- External API client  

➡ Tất cả những gì liên quan DB, network, file, cache → **vứt hết vào Infrastructure**.

</details>


<details>
<summary>✅ 4. Lưu ý về Presentation Layer</summary>

**Controller KHÔNG xử lý nghiệp vụ**, chỉ:  
1. Nhận request  
2. Gọi ApplicationService / Mediator  
3. Trả response  

**Presentation Layer gồm:**
- Controller  
- DTO (gọn, không logic)  
- Middleware  
- Filter/Attribute  
- Program.cs (config DI, pipeline, swagger, cors…)  

</details>


<details>
<summary>✅ 5. Lưu ý về Shared Layer</summary>

**Shared chứa các thành phần không phụ thuộc framework & dùng chung toàn solution:**

- Result&lt;T&gt;  
- Paging / Sorting  
- Custom exceptions  
- DateTimeProvider  
- IdGenerator  
- Guard clause  
- Helpers  

**❌ Không được đưa vào Shared:**
- EF  
- Repository  
- Connection string  
- API client  

</details>


<details>
<summary>🔥 Tổng kết nguyên tắc phụ thuộc</summary>

- **Domain KHÔNG phụ thuộc ai**  
- **Application phụ thuộc Domain**  
- **Presentation phụ thuộc Application**  
- **Infrastructure phụ thuộc Domain**  

➡ **Program.cs chỉ Inject interface → implementation**  
➡ **Không để logic nghiệp vụ ngoài Domain**  
➡ **Không để I/O ngoài Infrastructure**

</details>


Api/Presentation  →  Application  →  Domain
       ↓
Infrastructure  →  Domain
