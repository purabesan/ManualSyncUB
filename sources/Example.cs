using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
public class Example : PurabeWorks.ManualSyncUB //継承
{
    //Manual同期のサンプルコード

    //同期したい変数にはUdonSyncedをつける
    [UdonSynced(UdonSyncMode.None), SerializeField]
    private bool flag = false;

    public override void Interact()
    {
        //同期変数の変更
        flag = !flag;
        //同期処理の呼び出し
        Synchronize();
    }

    /* 同期変数更新後に行いたい共通処理はここに記述
     * 例:アニメーション遷移など */
    protected override void AfterSynchronize()
    {
        //何かの処理
        //例: someAnimator.SetBool("Flag", flag);
        return;
    }

    /* Late-joinerを気にせず実行できるグローバル処理
     * 例:サウンドSE再生など
     * ようするにSendCustomNetworkEventですが、
     * 長いので短く書けるようにしました */
    public void SomeGlobalAction()
    {
        //何かの処理
        //例: audioSource.PlayOneShot(audioClip);
        return;
    }

    public override void OnPickupUseDown()
    {
        ExecuteOnAll(nameof(SomeGlobalAction));
    }
}
