﻿namespace MicroServiceV2.File.API.Features.File.Delete
{
    public class DeleteFileCommandHandler(IFileProvider fileProvider) : IRequestHandler<DeleteFileCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            var fileInfo=fileProvider.GetFileInfo(Path.Combine("files",request.FileName));
            if (!fileInfo.Exists)
            {
                return ServiceResult.ErrorAsNotFount();
            }
            System.IO.File.Delete(fileInfo.PhysicalPath!);
            return ServiceResult.SuccessAsNoContent();

        }
    }
}
