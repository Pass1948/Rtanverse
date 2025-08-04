# 총알 프리팹 설정 가이드

## 1. 총알 프리팹 생성
1. Unity에서 빈 GameObject를 생성합니다.
2. 이름을 "Bullet"로 변경합니다.

## 2. 컴포넌트 추가
다음 컴포넌트들을 Bullet GameObject에 추가하세요:

### 필수 컴포넌트:
- **SpriteRenderer**: 총알 이미지 표시
- **Rigidbody2D**: 물리 시뮬레이션
  - Body Type: Dynamic
  - Gravity Scale: 0 (중력 영향 없음)
- **CircleCollider2D** 또는 **BoxCollider2D**: 충돌 감지
  - Is Trigger: true (트리거로 설정)
- **Bullet.cs**: 총알 동작 스크립트

## 3. 총알 이미지 설정
- SpriteRenderer에 총알 이미지를 할당하세요
- 크기는 적절히 조정하세요 (예: 0.5 x 0.5)

## 4. Bullet 스크립트 설정
- **Damage**: 총알 데미지 (기본값: 10)
- **Knockback Force**: 넉백 힘 (기본값: 5)
- **Target Layers**: 공격할 대상 레이어 설정

## 5. 프리팹으로 저장
- Bullet GameObject를 **Resources/Prefabs** 폴더에 프리팹으로 저장하세요
- 프리팹 이름을 "Bullet"로 설정하세요 (경로: Resources/Prefabs/Bullet)

## 6. PlayerControl 설정
PlayerControl 스크립트에서:
- **Bullet Prefab Path**: "Prefabs/Bullet" (자동으로 설정됨)
- **Fire Point**: 발사 위치 (플레이어 자식 오브젝트로 생성)
- **Bullet Speed**: 총알 속도 (기본값: 10)
- **Fire Rate**: 발사 간격 (기본값: 0.5초)
- **Bullet Lifetime**: 총알 생존 시간 (기본값: 3초)

## 7. 발사 위치 설정
1. 플레이어 오브젝트의 자식으로 빈 GameObject 생성
2. 이름을 "FirePoint"로 변경
3. 플레이어 앞쪽에 위치시키기
4. PlayerControl의 Fire Point에 할당

## 8. 입력 설정
- 이미 "Attack" 액션이 Input System에 설정되어 있음
- 마우스 왼쪽 버튼 또는 게임패드 X 버튼으로 발사 가능

## 9. 레이어 설정
- 총알이 공격할 대상들에 적절한 레이어 설정
- Bullet 스크립트의 Target Layers에서 해당 레이어 선택 