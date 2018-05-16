package com.mark.myshareplugin;

import android.content.Context;
import android.content.Intent;
import android.media.MediaScannerConnection;
import android.net.Uri;
import android.os.Build;

import android.support.v4.content.FileProvider;

import com.unity3d.player.UnityPlayer;

import java.io.File;

public class ShareWork {

    private Context context;

    public  ShareWork(Context context){
        this.context = context;
    }

    public void DoShare(String filePath, String text){
        final String shareText = text;
        if(Build.VERSION.SDK_INT>=Build.VERSION_CODES.N){
            String providerName = context.getPackageName()+".fileProvider";
            File file = new File(filePath);
            Uri imageUri = FileProvider.getUriForFile(context,providerName, file);

            Intent scanIntent = new Intent(Intent.ACTION_MEDIA_SCANNER_SCAN_FILE);
            scanIntent.setData(imageUri);
            context.sendBroadcast(scanIntent);
            MediaScannerConnection.scanFile(context,new String[] {filePath},null,
                    new MediaScannerConnection.OnScanCompletedListener() {
                        public void onScanCompleted(String path, Uri uri){
                            PublishIntent(uri, shareText);
                        }
                    });
        }else{
            Uri imageUri = Uri.parse("file://"+filePath);
            PublishIntent(imageUri, shareText);
        }
    }

    private void PublishIntent(Uri uri, String text){
        Intent intent = new Intent(Intent.ACTION_SEND);
        intent.putExtra(Intent.EXTRA_TEXT, text);
        intent.putExtra(Intent.EXTRA_STREAM, uri);
        intent.setType("image/png");
        intent.addFlags(Intent.FLAG_GRANT_READ_URI_PERMISSION);
        UnityPlayer.currentActivity.startActivity(Intent.createChooser(intent, "send"));
    }

}
