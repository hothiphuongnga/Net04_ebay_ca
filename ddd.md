## DDD - Domain Driven Design


domain: nghiệp vụ
driven : hướng xử ký
design : thiết kế

- Domain : là nơi chứa các nghiệp vụ chính của hệ thống
    Entities : phản chiếu table trong DB
    Value Object : đối tượng giá trị

        ví dụ : field email   Email email, nó là cố đinh và không bị ảnh hưởng bởi các nghiệp vụ khác
    Repository: interface : nghiệp vụ cần có chứ không có logic code 
- Application
    - Interface : IService
        Iorderservice
        ....

    - Service : 
        orderservice ...
    - DTOs
        ...
    

- Infrastructure    
    - Repository : thực thi interface trên domain
        OrderRepo
    - UnitOfWork

    - ExternalService
        - EmailService
        - FileService 

- Presentation / Api : 
    - API  tạo webapi
    - Blazor



Hướng 1: 1 solution 1 project, 1 project có 4 layer (folder) -> dễ cấu trúc hơn nhưng phù hợp dự án nhỏ 

Hướng 2: 1 sln -> 4 project (domain, application, infrastructure, api/presentation) -> phù hợp dự án lớn , dễ triển khai Microservice
     dễ test hơn, dễ bảo trì hơn
     


dotnet ef dbcontext scaffold "Name=ConnectionString" Microsoft.EntityFrameworkCore.SqlServer -o ../ebay.Domain/Entities --context-dir ../ebay.Domain/Data -f
