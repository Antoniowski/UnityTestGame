using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

class PixelPass : ScriptableRenderPass
    {
        private PixelPassFeature.CustomPassSettings settings;

        private RenderTargetIdentifier colorBuffer, pixelBuffer;
        private int pixelBufferId = Shader.PropertyToID("_PixelBuffer");

        private Material material;
        private int pixelScreenHeight, pixelScreenWidth;

        public PixelPass(PixelPassFeature.CustomPassSettings settings){
            this.settings = settings;
            this.renderPassEvent = settings.renderPassEvent;
            if(material == null) material = CoreUtils.CreateEngineMaterial("Hidden/Pixelize");
        } 
        // This method is called before executing the render pass.
        // It can be used to configure render targets and their clear state. Also to create temporary render target textures.
        // When empty this render pass will render to the active camera render target.
        // You should never call CommandBuffer.SetRenderTarget. Instead call <c>ConfigureTarget</c> and <c>ConfigureClear</c>.
        // The render pipeline will ensure target setup and clearing happens in a performant manner.
        public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
        {
            colorBuffer = renderingData.cameraData.renderer.cameraColorTarget;
            RenderTextureDescriptor descriptor = renderingData.cameraData.cameraTargetDescriptor;


            pixelScreenHeight = settings.screenHeight;
            pixelScreenWidth = (int) (pixelScreenHeight*renderingData.cameraData.camera.aspect + 0.5f);

            material.SetVector("_BlockCount", new Vector2(pixelScreenWidth, pixelScreenHeight));
            material.SetVector("_BlockSize", new Vector2(1.0f/pixelScreenWidth, 1.0f/pixelScreenHeight));
            material.SetVector("_HalfBlockSize", new Vector2(0.5f/pixelScreenWidth, 0.5f/pixelScreenHeight));

            descriptor.height = pixelScreenHeight;
            descriptor.width = pixelScreenWidth;

            cmd.GetTemporaryRT(pixelBufferId, descriptor, FilterMode.Point);
            pixelBuffer = new RenderTargetIdentifier(pixelBufferId);
        }

        // Here you can implement the rendering logic.
        // Use <c>ScriptableRenderContext</c> to issue drawing commands or execute command buffers
        // https://docs.unity3d.com/ScriptReference/Rendering.ScriptableRenderContext.html
        // You don't have to call ScriptableRenderContext.submit, the render pipeline will call it at specific points in the pipeline.
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get();
            using (new ProfilingScope(cmd, new ProfilingSampler("Pixelize Pass"))){
                Blit(cmd, colorBuffer, pixelBuffer, material);
                Blit(cmd, pixelBuffer, colorBuffer);
            }

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        // Cleanup any allocated resources that were created during the execution of this render pass.
        public override void OnCameraCleanup(CommandBuffer cmd)
        {
            if(cmd == null) throw new System.ArgumentNullException("cmd");
            cmd.ReleaseTemporaryRT(pixelBufferId);
        }
}
