using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

namespace PurabeWorks
{
    public class ManualSyncUB : UdonSharpBehaviour
    {
        /* 同期関連処理のまとめ */

        /// <summary>
        /// オーナ権限取得
        /// </summary>
        /// <param name="target">取得対象オブジェクト</param>
        protected void GetOwner(GameObject target = null)
        {
            if (target == null)
            {
                target = this.gameObject;
            }
            if (!Networking.IsOwner(target))
            {
                Networking.SetOwner(Networking.LocalPlayer, target);
            }
        }

        //Late Joiner対応
        public override void OnPlayerJoined(VRCPlayerApi player)
        {
            RequestSerialization();
        }

        //同期変数受信時の処理
        public override void OnDeserialization()
        {
            AfterSynchronize();
        }

        /// <summary>
        /// 同期化処理。オーナ権限の移動・同期指示側のAfterSynchronize実行を含む。
        /// </summary>
        protected void Synchronize()
        {
            GetOwner();
            RequestSerialization();
            AfterSynchronize();
        }

        /// <summary>
        /// 同期後の共通処理
        /// </summary>
        virtual protected void AfterSynchronize()
        {
            return;
        }

        /// <summary>
        /// グローバル処理実行。SendCustomNetworkEvent Allするだけ。
        /// </summary>
        /// <param name="eventName">実行したいpublic関数。nameof(関数名)にて指定</param>
        protected void ExecuteOnAll(string eventName)
        {
            SendCustomNetworkEvent(
               VRC.Udon.Common.Interfaces.NetworkEventTarget.All,
               eventName);
        }
    }
}
