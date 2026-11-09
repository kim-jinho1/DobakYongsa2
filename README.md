# 도박용사 2

집을 사려 던전과 도박에 뛰어든 용사의 이야기를 3D 액션으로 재구현한 개인 프로젝트입니다.
2025년도 1학기 게임 엔진응용 개인 프로젝트입니다.

- **포트폴리오**: [도박용사 2 (노션)](https://www.notion.so/316048b7ae3480909b2ffaa19106d226)
- **플레이 영상**: https://youtu.be/Q_HqNgOLCiE
- **인원 · 기간**: 1명 · 2025.05.27 ~ 2025.06.14
- **엔진**: Unity 6 (6000.0.46f1) · C# · 3인칭 백뷰 3D 액션

## 먼저 볼 코드

기준 폴더는 `Assets/01.Code/`입니다.

- **콤보 공격** — 입력 시간 창(`comboWindow`) 안에 이어지면 최대 3연속 콤보로 이어집니다
  - `Players/Components/PlayerAttackCompo.cs` · `Players/States/PlayerAttackState.cs`
- **타격감** — 피격 피드백(히트 이펙트는 풀에서 꺼내 씀)과 카메라 셰이크. 히트스톱 · 슬로모션 조합은 포트폴리오에 정리했습니다
  - `Feedbacks/` · `CameraSetting/CameraShake.cs`
- **적 AI** — 수업에서 배운 Behavior Tree 구조 위에 노드를 구성했습니다 (순찰 → 추적 → 공격, 시야 · 공격 범위 판정은 Condition 노드)
  - `BT/`
- **야바위** — 컵 세 개 중 하나를 고르는 미니게임
  - `DoBak/` · `Players/Components/DoBakCompo.cs`

## 출처

작성 주체별 구분입니다.

| 부분 | 출처 |
|---|---|
| FSM · 엔티티 · 스탯 · 전투 기본 구조 · BT 노드 틀 (`FSM/` · `Entities/` · `Core/StatSystem/` · `Combat/` · `BT/`) | 수업 코드를 기반으로 사용 |
| `Assets/GondrLib` (오브젝트 풀 · 의존성 주입) | 수업 라이브러리 |

## 의존성

Unity Behavior · DOTween · Input System · GondrLib(수업 라이브러리)
