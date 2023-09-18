# ManualSyncUB
ManualSyncUB; Manual Synchronized Udon Behaviour は、VRChat VCC/U#1.0 向けに作成された、継承用の親クラスです。  
Late-joiner対応のManual同期を簡単にできるよう、最低限のシンプルな構成としています。  
Late-joiner対応のManual UdonSynced変数同期、およびUdonSynced変数の更新に応じて何らかの処理をしたい、自分でU#; UdonSharpを書ける方、是非ご利用ください。

[コードサンプルはこちら](sources/Example.cs)

## 使い方

1. UnityのProjectウィンドウで、任意の場所に [ManualSyncUB.cs](sources/ManualSyncUB.cs) をドラッグ&ドロップ
2. 新規にU# Scriptを作成
3. 生成された.csファイルを開く
4. 継承元のクラスを UdonSharpBehaviour から PurabeWorks.ManualSyncUB に変更
5. 中身の処理を書いていく

## 関数の説明

* protected void Synchronize()  
オーナ権限まわりの処理も含め、同期処理を一通りこなす関数。UdonSynced変数を変更したあとに呼び出す。

* protected void AfterSynchronize()  
UdonSynced変数が更新されたあとにやりたい処理をこの変数に override することで、任意の同期処理を実行できます。  
ここで指定した処理は、Late-joinerにも反映されます。  
Animator の変数変更などはここがオススメです。

* protected void ExecuteOnAll(string eventName)  
Late-joiner対応不要のグローバル処理を実行します。ようするに SendCustomNetworkEvent All なのですが、長いので関数化しました。  
実行させたい処理をpublic関数として作成し、 ExecuteOnAll(nameof(関数名)) の形式で呼び出してください。  
文字列指定してもOKですが、 nameof を使った方が、関数名を変更したときにリファクタリングが効いて便利です。  
サウンドSEの再生などはここがオススメです。

## 詳しい説明については

はてなブログで解説しておりますので、よろしければご参照ください。

[【U#;UdonSharp】Manual同期のUdonSynced変数を完全に理解する【VRChat/SDK3】](https://purabe.hatenablog.com/entry/2021/11/30/211613)
