#! /bin/sh

PROJECT_PATH=$(pwd)
UNITY_BUILD_DIR=$(pwd)/Build

ERROR_CODE=1

echo "Running editor test..."
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile \
  -projectPath "$PROJECT_PATH" \
  -buildTarget "Android" \
  -username "$UNITYEMAIL" \
  -password "$UNITYPASSWORD" \
  -serial "$UNITYKEY" \
  -runEditorTests |
  tee "$LOG_FILE"

if [ $? = 0 ]; then
  echo "Editor tests Passed."
  ERROR_CODE=0
else
  echo "One or more Editor test failed $?."
  ERROR_CODE=1
fi

echo "return license"
/Applications/Unity/Unity.app/Contents/MacOS/Unity -quit -batchmode -returnlicense

exit $ERROR_CODE
