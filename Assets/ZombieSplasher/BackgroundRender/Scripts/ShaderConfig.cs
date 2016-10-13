// Copyright (C) 2016 Filip Cyrus Bober

namespace FCB.BackgroundRender
{
    public static class ShaderConfig
    {
        public static class GradientShader
        {
            public const string BgColorReference = "_BgColor";
            public const string BgDepthReference = "_BgDepth";

            public const string GradientReference = "_Gradient";
            public const float GradientDefaultValue = -0.18f;

            public const string DepthColorView = "_DepthView";
            public const float DepthViewOn = 1f;
            public const float DepthViewOff = 0f;
        }

        public static class BackgroundRenderShader
        {
            public const string ColorCameraView = "_Color";
            public const string DepthCameraView = "_Depth";
        }
    }
}
