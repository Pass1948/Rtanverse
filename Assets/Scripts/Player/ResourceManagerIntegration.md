# ResourceManager를 활용한 총알 시스템

## 개요
기존의 `Instantiate()`와 `Destroy()` 대신 `ResourceManager`를 사용하여 총알 시스템을 재구성했습니다.

## 주요 변경사항

### 1. PlayerControl.cs
- **총알 프리팹 참조 방식 변경**: 
  - 기존: `[SerializeField] GameObject bulletPrefab`
  - 변경: `[SerializeField] string bulletPrefabPath = "Prefabs/Bullet"`
- **총알 생성**: `GameManager.Resource.Instantiate<GameObject>(bulletPrefabPath, position, rotation, true)`
- **총알 삭제**: `GameManager.Resource.Destroy(bullet, lifetime)`

### 2. Bullet.cs
- **총알 제거**: 모든 `Destroy(gameObject)` → `GameManager.Resource.Destroy(gameObject)`

### 3. DamageableObject.cs
- **객체 파괴**: `Destroy(gameObject)` → `GameManager.Resource.Destroy(gameObject)`

## ResourceManager의 장점

### 1. **오브젝트 풀링**
- 총알을 매번 생성/삭제하지 않고 풀에서 재사용
- 메모리 할당/해제 최소화로 성능 향상
- 가비지 컬렉션 부하 감소

### 2. **리소스 캐싱**
- 프리팹을 한 번 로드하면 메모리에 캐시
- 반복적인 리소스 로딩 방지
- 빠른 오브젝트 생성

### 3. **자동 관리**
- 풀링된 오브젝트의 자동 반환
- 메모리 누수 방지
- 효율적인 리소스 관리

## 작동 방식

### 총알 생성 과정
1. `GameManager.Resource.Instantiate()` 호출
2. ResourceManager가 "Prefabs/Bullet" 경로에서 프리팹 로드
3. 풀링 옵션이 true이므로 PoolManager에서 총알 생성/재사용
4. 총알에 속도와 방향 적용

### 총알 제거 과정
1. 충돌, 화면 밖 이동, 시간 초과 시 `GameManager.Resource.Destroy()` 호출
2. ResourceManager가 PoolManager를 통해 총알을 풀로 반환
3. 실제 삭제가 아닌 비활성화로 메모리 효율성 확보

## 설정 요구사항

### 1. 프리팹 위치
```
Assets/Resources/Prefabs/Bullet.prefab
```

### 2. PoolManager 설정
- 총알 프리팹이 PoolManager에 등록되어야 함
- 초기 풀 크기 설정 권장

### 3. GameManager 참조
- 모든 스크립트에서 `GameManager.Resource` 접근 가능해야 함

## 성능 최적화 효과

### 메모리 사용량
- 기존: 매번 새로운 GameObject 생성
- 개선: 풀링된 오브젝트 재사용

### CPU 사용량
- 기존: 반복적인 Instantiate/Destroy 호출
- 개선: 풀에서 빠른 활성화/비활성화

### 가비지 컬렉션
- 기존: 빈번한 GC 호출
- 개선: GC 부하 최소화

## 확장 가능성

### 다양한 총알 타입
```csharp
// 다른 총알 타입 추가 가능
[SerializeField] string normalBulletPath = "Prefabs/Bullet";
[SerializeField] string fireBulletPath = "Prefabs/FireBullet";
[SerializeField] string iceBulletPath = "Prefabs/IceBullet";
```

### 풀링 설정
```csharp
// 풀링 사용 여부 선택 가능
GameManager.Resource.Instantiate<GameObject>(bulletPath, position, rotation, usePooling);
```

이러한 구조로 인해 게임의 성능이 크게 향상되고, 메모리 관리가 효율적으로 이루어집니다. 