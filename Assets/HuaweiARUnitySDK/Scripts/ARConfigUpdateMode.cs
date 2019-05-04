namespace HuaweiARUnitySDK
{
    /**
     * \if english
     * @brief Selects the behavior of \link ARSession.Update()\endlink.
     * \else
     * @brief 设置 \link ARSession.Update()\endlink 的行为。
     * \endif
     */
    public enum ARConfigUpdateMode
    {
        /**
         * \if english
         * @brief \link ARSession.Update()\endlink will wait until a new camera image is available. 
         * <b>Note: If the camera image does not arrive by the built-in timeout(66ms), then <a>ARSession.Update()</a> will return
         * the most recent \link ARFrame\endlink instead.</b>
         * \else
         * @brief \link ARSession.Update()\endlink 将会阻塞至下一帧预览可用。<b>注意：如果预览超时（66ms），<a>ARSession.Update()</a>
         * 将会返回最近的一次\link ARFrame\endlink</b>。
         * \endif
         */
        BLOCKING = 0,

        /**
         * \if english
         * @brief \link ARSession.Update()\endlink will return immediately without blocking.
         * If no new camera image is available, then <a>ARSession.Update()</a> will return
         * the most recent \link ARFrame\endlink instead.
         * \else
         * @brief \link ARSession.Update()\endlink 立刻返回。如果没有更新的帧可用，<a>ARSession.Update()</a>
         * 将会返回最近的一次\link ARFrame\endlink。
         * \endif
         */
        LATEST_CAMERA_IMAGE = 1,
    }
}
