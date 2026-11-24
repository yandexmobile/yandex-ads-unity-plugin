#!/bin/sh
# Copy SwiftPM resources without linking static SDKs into the application again.
set -eu
resources="${TARGET_BUILD_DIR}/${UNLOCALIZED_RESOURCES_FOLDER_PATH}"
mkdir -p "${resources}"
for bundle in "${BUILT_PRODUCTS_DIR}"/*.bundle; do
    [ -d "${bundle}" ] || continue
    /usr/bin/rsync -aLc --delete "${bundle}" "${resources}/"
done
