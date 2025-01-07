# Pratik - Identity and Data Protection

## Kullanıcı Yönetimi ve Veritabanı Oluşturma

Bu proje, bir kullanıcı tablosu oluşturmayı, kullanıcı kayıtlarını Entity Framework Code First yaklaşımıyla veritabanına kaydetmeyi ve şifreleme işlemlerini gerçekleştirmeyi amaçlar. Ayrıca, model doğrulaması (validation) yapılarak kullanıcı verilerinin güvenliği sağlanmıştır.

---

## Proje Adımları

### 1. **User Tablosunun Oluşturulması**
User tablosu aşağıdaki alanlara sahiptir:

- **Email**: Kullanıcının e-posta adresini saklar.
- **Password**: Kullanıcının şifresini saklar (şifrelenmiş olarak tutulacaktır).

Entity Framework kullanılarak Code First yöntemiyle bu tablo oluşturulmuştur.

**Model:**
```csharp
public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
    public string Password { get; set; }
}
```

---

### 2. **Veritabanı Oluşturulması**

Entity Framework Core kullanılarak veritabanı Code First yaklaşımıyla oluşturulmuştur. `DbContext` sınıfı aşağıdaki gibidir:

**DbContext:**
```csharp
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
}
```

**Migration ve Veritabanı Oluşturma Komutları:**
```bash
# Migration eklenmesi
dotnet ef migrations add InitialMigration

# Veritabanının güncellenmesi
dotnet ef database update
```

---

### 3. **Şifreleme (Password Encryption)**

Kullanıcının şifresi, veritabanına kaydedilmeden önce şifrelenmiştir. Şifreleme için **Data Protection API** kullanılmıştır.

**Şifreleme İşlemi:**
```csharp
public class PasswordHasher
{
    private readonly IDataProtector _protector;

    public PasswordHasher(IDataProtectionProvider provider)
    {
        _protector = provider.CreateProtector("PasswordEncryption");
    }

    public string EncryptPassword(string password)
    {
        return _protector.Protect(password);
    }

    public string DecryptPassword(string encryptedPassword)
    {
        return _protector.Unprotect(encryptedPassword);
    }
}
```

Şifreleme işlemi, kullanıcı kayıt edilirken uygulanır.

---

### 4. **Model Validation**

Kullanıcıdan gelen veriler, `Data Annotations` kullanılarak doğrulanmıştır. 

**Doğrulama Örnekleri:**
- Email alanı `Required` ve `EmailAddress` ile işaretlenmiştir.
- Password alanı `Required` ve minimum 8 karakter uzunlukta olmalıdır.

Kayıt sırasında doğrulama hataları, API tarafından döndürülmektedir.

---

### 5. **API Endpointleri**

#### Kullanıcı Kaydı (Register)
Kullanıcının sisteme kaydolmasını sağlar.

**HTTP POST:** `/api/users/register`

**Request Body:**
```json
{
    "email": "example@example.com",
    "password": "securepassword"
}
```

**Response:**
- Başarılı: `201 Created`
- Hatalı: `400 Bad Request`

---

### 6. **Projeyi Çalıştırma**

1. **Bağımlılıkları Yükleyin:**
   ```bash
   dotnet restore
   ```

2. **Migration ve Veritabanı Güncelleme:**
   ```bash
   dotnet ef migrations add InitialMigration
   dotnet ef database update
   ```

3. **Projeyi Başlatın:**
   ```bash
   dotnet run
   ```

4. API endpointlerini test etmek için Postman veya benzeri bir araç kullanabilirsiniz.

---

## Gereksinimler

- .NET 6 veya üzeri
- Entity Framework Core
- Data Protection API

---

## Kaynaklar

- [Entity Framework Core Documentation](https://learn.microsoft.com/en-us/ef/core/)
- [Microsoft Data Protection API](https://learn.microsoft.com/en-us/aspnet/core/security/data-protection/)

---
