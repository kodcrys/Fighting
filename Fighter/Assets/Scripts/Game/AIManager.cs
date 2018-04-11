using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour {

	public enum DifficultLevel {none, Easy, Normal, Hard, WhatTheFuckIamPlaying, Tor1, Tor2, Tor3, Tor4}

	public DifficultLevel difLevel = DifficultLevel.none;

	[SerializeField]
	bool left, right;

	[SerializeField]
	GameObject finger;

	[SerializeField]
	FingerLeftControl fingerAILeft;

	[SerializeField]
	FingerRightControl fingerAIRight;

	[SerializeField]
	int ranMove;

	[SerializeField]
	float time, timeInter;

	void Awake(){
		if (SaveManager.instance.state.levelAI == 0) {
			difLevel = DifficultLevel.Easy;
		} else if (SaveManager.instance.state.levelAI == 1) {
			difLevel = DifficultLevel.Normal;
		} else if (SaveManager.instance.state.levelAI == 2) {
			difLevel = DifficultLevel.Hard;
		} else if (SaveManager.instance.state.levelAI == 3) {
			difLevel = DifficultLevel.WhatTheFuckIamPlaying;
		} else if (SaveManager.instance.state.levelAI == 4) {
			difLevel = DifficultLevel.Tor1;
		} else if (SaveManager.instance.state.levelAI == 5) {
			difLevel = DifficultLevel.Tor2;
		} else if (SaveManager.instance.state.levelAI == 6) {
			difLevel = DifficultLevel.Tor3;
		} else if (SaveManager.instance.state.levelAI == 7) {
			difLevel = DifficultLevel.Tor4;
		}

		if (left)
			fingerAILeft = finger.GetComponent<FingerLeftControl> ();
		else if (right)
			fingerAIRight = finger.GetComponent<FingerRightControl> ();
	}

	// Use this for initialization
	void Start () {
		
	}

	void OnEnable(){
		if (difLevel == DifficultLevel.WhatTheFuckIamPlaying) {
			if (left)
				fingerAILeft.fuckingMode = true;
			else if (right)
				fingerAIRight.fuckingMode = true;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (AnimationText.canPlay) {
			if (!GameplayBase.instance.gamePause) {
				time += Time.deltaTime;
				switch (difLevel) {
				case DifficultLevel.Easy:
					EasyMode ();
					break;
				case DifficultLevel.Normal:
					NormalMode ();
					break;
				case DifficultLevel.Hard:
					HardMode ();
					break;
				case DifficultLevel.WhatTheFuckIamPlaying:
					FuckingMode ();
					break;
				case DifficultLevel.Tor1:
					Tor1Mode ();
					break;
				case DifficultLevel.Tor2:
					Tor2Mode ();
					break;
				case DifficultLevel.Tor3:
					Tor3Mode ();
					break;
				case DifficultLevel.Tor4:
					Tor4Mode ();
					break;
				}
			}
		}
	}

	void EasyMode(){
		timeInter = 0.4f;
		if (time >= timeInter) {
			if (right) {
				if (!fingerAIRight.isAtk) {
					if (fingerAIRight.enemyLeft.fingerAction == FingerBase.FingerState.Idel) {
						ranMove = Random.Range (0, 100);
						if (ranMove > 50) {
							AIClick ();
						} else {
							AIUnClick ();
						}
					} else {
						ranMove = Random.Range (0, 100);
						if (ranMove > 70) {
							AIClick ();
						} else {
							AIUnClick ();
						}
					}
				}
			} else if (left) {
				if (!fingerAILeft.isAtk) {
					if (fingerAILeft.enemyRight.fingerAction == FingerBase.FingerState.Idel) {
						ranMove = Random.Range (0, 100);
						if (ranMove > 50) {
							AIClick ();
						} else {
							AIUnClick ();
						}
					} else {
						ranMove = Random.Range (0, 100);
						if (ranMove > 70) {
							AIClick ();
						} else {
							AIUnClick ();
						}
					}
				}
			}
			time = 0;
		}
	}

	void NormalMode(){
		timeInter = 0.2f;
		if (time >= timeInter) {
			if (right) {
				if (!fingerAIRight.isAtk) {
					if (fingerAIRight.enemyLeft.fingerAction == FingerBase.FingerState.Idel) {
						ranMove = Random.Range (0, 100);
						if (ranMove > 50) {
							AIClick ();
						} else {
							AIUnClick ();
						}
					} else {
						ranMove = Random.Range (0, 100);
						if (ranMove > 50) {
							AIClick ();
						} else {
							AIUnClick ();
						}
					}
				}
			} else if (left) {
				if (!fingerAILeft.isAtk) {
					if (fingerAILeft.enemyRight.fingerAction == FingerBase.FingerState.Idel) {
						ranMove = Random.Range (0, 100);
						if (ranMove > 50) {
							AIClick ();
						} else {
							AIUnClick ();
						}
					} else {
						ranMove = Random.Range (0, 100);
						if (ranMove > 50) {
							AIClick ();
						} else {
							AIUnClick ();
						}
					}
				}
			}
			time = 0;
		}
	}

	void HardMode(){
		timeInter = 0.1f;
		if (time >= timeInter) {
			if (right) {
				if (!fingerAIRight.isAtk) {
					if (fingerAIRight.enemyLeft.fingerAction == FingerBase.FingerState.Idel) {
						ranMove = Random.Range (0, 100);
						if (ranMove > 50) {
							AIClick ();
						} else {
							AIUnClick ();
						}
					} else {
						ranMove = Random.Range (0, 100);
						if (ranMove > 20) {
							AIClick ();
						} else {
							AIUnClick ();
						}
					}
				}
			} else if (left) {
				if (!fingerAILeft.isAtk) {
					if (fingerAILeft.enemyRight.fingerAction == FingerBase.FingerState.Idel) {
						ranMove = Random.Range (0, 100);
						if (ranMove > 50) {
							AIClick ();
						} else {
							AIUnClick ();
						}
					} else {
						ranMove = Random.Range (0, 100);
						if (ranMove > 20) {
							AIClick ();
						} else {
							AIUnClick ();
						}
					}
				}
			}
			time = 0;
		}
	}

	void FuckingMode(){
		timeInter = 0.1f;
		if (time >= timeInter) {
			if (right) {
				if (!fingerAIRight.isAtk) {
					if (fingerAIRight.enemyLeft.fingerAction == FingerBase.FingerState.Idel) {
						ranMove = Random.Range (0, 100);
						if (ranMove > 50) {
							AIClick ();
						} else {
							AIUnClick ();
						}
					} else {
						ranMove = Random.Range (0, 100);
						if (ranMove > 20) {
							AIClick ();
						} else {
							AIUnClick ();
						}
					}
				}
			} else if (left) {
				if (!fingerAILeft.isAtk) {
					if (fingerAILeft.enemyRight.fingerAction == FingerBase.FingerState.Idel) {
						ranMove = Random.Range (0, 100);
						if (ranMove > 50) {
							AIClick ();
						} else {
							AIUnClick ();
						}
					} else {
						ranMove = Random.Range (0, 100);
						if (ranMove > 20) {
							AIClick ();
						} else {
							AIUnClick ();
						}
					}
				}
			}
			time = 0;
		}
	}

	void Tor1Mode(){
		timeInter = 0.35f;
		if (time >= timeInter) {
			if (right) {
				if (!fingerAIRight.isAtk) {
					if (fingerAIRight.enemyLeft.fingerAction == FingerBase.FingerState.Idel) {
						ranMove = Random.Range (0, 100);
						if (ranMove > 50) {
							AIClick ();
						} else {
							AIUnClick ();
						}
					} else {
						ranMove = Random.Range (0, 100);
						if (ranMove > 50) {
							AIClick ();
						} else {
							AIUnClick ();
						}
					}
				}
			} else if (left) {
				if (!fingerAILeft.isAtk) {
					if (fingerAILeft.enemyRight.fingerAction == FingerBase.FingerState.Idel) {
						ranMove = Random.Range (0, 100);
						if (ranMove > 50) {
							AIClick ();
						} else {
							AIUnClick ();
						}
					} else {
						ranMove = Random.Range (0, 100);
						if (ranMove > 50) {
							AIClick ();
						} else {
							AIUnClick ();
						}
					}
				}
			}
			time = 0;
		}
	}

	void Tor2Mode(){
		timeInter = 0.3f;
		if (time >= timeInter) {
			if (right) {
				if (!fingerAIRight.isAtk) {
					if (fingerAIRight.enemyLeft.fingerAction == FingerBase.FingerState.Idel) {
						ranMove = Random.Range (0, 100);
						if (ranMove > 50) {
							AIClick ();
						} else {
							AIUnClick ();
						}
					} else {
						ranMove = Random.Range (0, 100);
						if (ranMove > 50) {
							AIClick ();
						} else {
							AIUnClick ();
						}
					}
				}
			} else if (left) {
				if (!fingerAILeft.isAtk) {
					if (fingerAILeft.enemyRight.fingerAction == FingerBase.FingerState.Idel) {
						ranMove = Random.Range (0, 100);
						if (ranMove > 50) {
							AIClick ();
						} else {
							AIUnClick ();
						}
					} else {
						ranMove = Random.Range (0, 100);
						if (ranMove > 50) {
							AIClick ();
						} else {
							AIUnClick ();
						}
					}
				}
			}
			time = 0;
		}
	}

	void Tor3Mode(){
		timeInter = 0.25f;
		if (time >= timeInter) {
			if (right) {
				if (!fingerAIRight.isAtk) {
					if (fingerAIRight.enemyLeft.fingerAction == FingerBase.FingerState.Idel) {
						ranMove = Random.Range (0, 100);
						if (ranMove > 50) {
							AIClick ();
						} else {
							AIUnClick ();
						}
					} else {
						ranMove = Random.Range (0, 100);
						if (ranMove > 50) {
							AIClick ();
						} else {
							AIUnClick ();
						}
					}
				}
			} else if (left) {
				if (!fingerAILeft.isAtk) {
					if (fingerAILeft.enemyRight.fingerAction == FingerBase.FingerState.Idel) {
						ranMove = Random.Range (0, 100);
						if (ranMove > 50) {
							AIClick ();
						} else {
							AIUnClick ();
						}
					} else {
						ranMove = Random.Range (0, 100);
						if (ranMove > 50) {
							AIClick ();
						} else {
							AIUnClick ();
						}
					}
				}
			}
			time = 0;
		}
	}

	void Tor4Mode(){
		timeInter = 0.2f;
		if (time >= timeInter) {
			if (right) {
				if (!fingerAIRight.isAtk) {
					if (fingerAIRight.enemyLeft.fingerAction == FingerBase.FingerState.Idel) {
						ranMove = Random.Range (0, 100);
						if (ranMove > 50) {
							AIClick ();
						} else {
							AIUnClick ();
						}
					} else {
						ranMove = Random.Range (0, 100);
						if (ranMove > 50) {
							AIClick ();
						} else {
							AIUnClick ();
						}
					}
				}
			} else if (left) {
				if (!fingerAILeft.isAtk) {
					if (fingerAILeft.enemyRight.fingerAction == FingerBase.FingerState.Idel) {
						ranMove = Random.Range (0, 100);
						if (ranMove > 50) {
							AIClick ();
						} else {
							AIUnClick ();
						}
					} else {
						ranMove = Random.Range (0, 100);
						if (ranMove > 50) {
							AIClick ();
						} else {
							AIUnClick ();
						}
					}
				}
			}
			time = 0;
		}
	}

	void AIClick(){
		if (left) {
			fingerAILeft.doingSomething = true;
			if (fingerAILeft.touch) {
				if (!fingerAILeft.isAtk)
					fingerAILeft.DoAtk ();
			}
		} else if (right) {
			fingerAIRight.doingSomething = true;
			if (fingerAIRight.touch) {
				if (!fingerAIRight.isAtk)
					fingerAIRight.DoAtk ();
			}
		}
	}

	void AIUnClick(){
		if (left) {
			if (fingerAILeft.fingerAction != FingerBase.FingerState.Idel) {
				fingerAILeft.UnClickAtk ();
			}
		} else if (right) {
			if (fingerAIRight.fingerAction != FingerBase.FingerState.Idel)
				fingerAIRight.UnClickAtk ();
		}
	}
}