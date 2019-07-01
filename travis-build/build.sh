#! /bin/sh

PROJECT_PATH=$(pwd)
UNITY_BUILD_DIR=$(pwd)/Build
LOG_FILE=$UNITY_BUILD_DIR/unity-android.log
UNITY_BUILD_APK_NAME=dev_travis.apk
UNITY_BUILD_APK_PATH=$PROJECT_PATH/Builds/Android/Development
UNITY_BUILD_APK=$UNITY_BUILD_APK_PATH/$UNITY_BUILD_APK_NAME

ERROR_CODE=1
echo "Items in project path ($PROJECT_PATH):"
ls "$PROJECT_PATH"

echo "Building project for Android..."
mkdir $UNITY_BUILD_DIR
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  --args buildName $UNITY_BUILD_APK_NAME \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile \
  -projectPath "$PROJECT_PATH" \
  -buildTarget "Android" \
  -username "$UNITYEMAIL" \
  -password "$UNITYPASSWORD" \
  -serial "$UNITYKEY" \
  -executeMethod "Infrastructure.EditorHelpers.Builder.BuildDevForAndroid" |
  tee "$LOG_FILE"

if [ $? = 0 ]; then
  echo "Building Android apk completed successfully."
  ERROR_CODE=0
else
  echo "Building Android apk failed. Exited with $?."
  ERROR_CODE=1
fi

echo "Finishing with code $ERROR_CODE"
exit $ERROR_CODE
