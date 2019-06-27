#! /bin/sh

PROJECT_PATH=$(pwd)
UNITY_BUILD_DIR=$(pwd)/Build
LOG_FILE=$UNITY_BUILD_DIR/unity-android.log


ERROR_CODE=0
echo "Items in project path ($PROJECT_PATH):"
ls "$PROJECT_PATH"


echo "Building project for Android..."
mkdir $UNITY_BUILD_DIR
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile \
  -projectPath "$PROJECT_PATH" \
  -buildTarget "Android" \
  -executeMethod "Infrastructure.EditorHelpers.Builder.BuildDevForAndroid"
  -quit \
  | tee "$LOG_FILE"
  
if [ $? = 0 ] ; then
  echo "Building Android apk completed successfully."
  ERROR_CODE=0
else
  echo "Building Android apk failed. Exited with $?."
  ERROR_CODE=1
fi

#echo 'Build logs:'
#cat $LOG_FILE

echo "Finishing with code $ERROR_CODE"
exit $ERROR_CODE
